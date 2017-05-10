# AppVeyor status badges (using Shield.io badges)
A simple webapi service that gets the detailed data from AppVeyor project and returns formated status badge. For badge creation, https://img.shields.io API is used.

## AppVeyor Tests Status
#### Examples
[![This is example static reference to shields.io api](https://img.shields.io/badge/tests-1582%20passing-brightgreen.svg)](https://img.shields.io/badge/tests-1582%20passing-brightgreen.svg)
[![This is example static reference to shields.io api](https://img.shields.io/badge/tests-12%20failed-red.svg)](https://img.shields.io/badge/tests-12%20failed-red.svg)

Badge that displays last build test status on master branch. 2 styles (green and red) for successful / failed runs, with passing/failing tests occurrence count.
Data is collected by appveyor WebAPI on `api/projects/{username}/{projectSlug}/branch/{branchName}` endpoint.
#### Usage in github (Markdown)

```
[![Tests status](<this should be address of deployed webservice>/api/api/testResults/<USERNAME>/<PROJECTSLUG>/badge.svg)](https://ci.appveyor.com/project/<USERNAME>/<PROJECTSLUG>)
```
Please replace `<USERNAME>` and `<PROJECTSLUG>` with your appveyor username and your project slug / name, for example:
```
[![Tests status](<this should be address of deployed webservice>/api/testResults/monkey3310/netcore-teamcity-api/badge.svg)](https://ci.appveyor.com/project/monkey3310/netcore-teamcity-api)
```



