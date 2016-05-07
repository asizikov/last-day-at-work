open Octokit
open System

let token = "xxx"
let organization = "org_name"
let user = "user_name"

let getAllReposForOrganization (client : GitHubClient) = async {
    let! repos = client.Repository.GetAllForOrg(organization)  |> Async.AwaitTask
    return repos
}

let contributedTo (client : GitHubClient, repo : Repository) = async {
    let! contributors = client.Repository.Statistics.GetContributors(repo.Owner.Login,repo.Name) 
                        |> Async.AwaitTask
    let result = match contributors 
                        |> Seq.tryFind (fun contributor -> contributor.Author.Login = user) with 
                                                            | Some contributor -> Some (repo.FullName, contributor.Total)
                                                            | None -> None
    return result
}

[<EntryPoint>]
let main argv = 
    let credentials = new Credentials(token);
    let client = new GitHubClient(new ProductHeaderValue("tool"));
    client.Credentials <- credentials

    let repos = client 
                |> getAllReposForOrganization 
                |> Async.RunSynchronously
    let reposContributedTo = Async.Parallel [for repo in repos ->  contributedTo(client, repo) ]
                             |> Async.RunSynchronously
                             |> Seq.filter (fun option -> option.IsSome)
    let quantity = Seq.length reposContributedTo
    let commits = reposContributedTo 
                    |> Seq.map (fun x -> x.Value)
                    |> Seq.sumBy (fun x -> snd x)
    printf "Contributed to %d repos, and made %d commits in total" quantity commits
        |> Console.WriteLine
    Console.ReadLine() |> ignore
    0 // return an integer exit code


