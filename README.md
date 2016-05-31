# last-day-at-work


A small tool I wrote to gather some statistics from GitHub.

You know, last day at work, before deleting all the access keys and leaving an organization.

The tool doesn't do much: just gets a list of repositories for the organization, gets the number of your commits to each repo and sums it up.

<img src="https://raw.githubusercontent.com/asizikov/last-day-at-work/master/content/inaction.png" width="800"/>

## How To Use

* First you need to create an [api token](https://github.com/blog/1509-personal-api-tokens)
* Store it as an environment variable as `LAST_DAY_AT_WORK_OCTOKIT_OAUTHTOKEN`
* Set you name and organization in the `organization` and `user` variables.