# Server

## Install express
$npm i express ts-node-dev typescript
$npm i @types/express --save-dev

## Install GraphQL
$npm install graphql express-graphql

https://developers.themoviedb.org/3/account/get-account-details

apikey: 7ef4a8b8370f25f7702f8cbce6825e7b
example: https://api.themoviedb.org/3/movie/550?api_key=7ef4a8b8370f25f7702f8cbce6825e7b

`
# Get movies list
https://api.themoviedb.org/3/movie/popular?api_key=7ef4a8b8370f25f7702f8cbce6825e7b&language=en-US&page=1

# Movies Ids: Spider-Man: No Way Home, id: 634649
## Get detail
https://api.themoviedb.org/3/movie/634649?api_key=7ef4a8b8370f25f7702f8cbce6825e7b&language=en-US
## Get external id
https://api.themoviedb.org/3/movie/634649/external_ids?api_key=7ef4a8b8370f25f7702f8cbce6825e7b
{
"id": 634649,
"imdb_id": "tt10872600",
"facebook_id": "SpiderManMovie",
"instagram_id": "spidermanmovie",
"twitter_id": "spidermanmovie"
}

## Find from extenal source, "imdb_id": "tt10872600"
https://api.themoviedb.org/3/find/tt10872600?api_key=7ef4a8b8370f25f7702f8cbce6825e7b&language=en-US&external_source=imdb_id
`

# Sample Query

## Aliases
`
{
  noWayHome: movie(id: 634649){
    id
    title
  }
  farFromHome: movie(id: 429617) {
    id
    title
  }
}
`

## Fragments
`
query SpidermanComparison {
  leftComparison: movie(id: 634649){
    ...comparisonFields
  }
  rightComparison: movie(id: 429617) {
    ...comparisonFields
  }
    
}

fragment comparisonFields on Movie {
  id
  title
  director{
    name
  }
}
`
## Variable
Use when want to dynamic post variable to server

Sample body request:
`
{
	"query":"query MovieAndCasts($id: ID) {\n  movie(id: $id) {\n    id\n    title\n    casts{\n      name\n    }\n  }\n}\n",
	"variables":{"id":634649},
	"operationName":"MovieAndCasts"
}
`
Query:
`
query MovieAndCasts($id: ID) {
  movie(id: $id) {
    id
    title
    casts{
      name
    }
  }
}
`
Variable:
`
{
  "id": 634649
}
`

## Directives
`
query MovieAndCasts($id: ID, $withCasts: Boolean!, $skipDirector: Boolean = false) {
  movie(id: $id) {
    id
    title
    casts @include (if: $withCasts) {
      name
    }
    director @skip (if: $skipDirector) {
      name
    }
  }
}
`

`
{
  "id": 634649,
  "withCasts": false,
  "skipDirector": true
}
`

## Mutation
### Create new dummy item
`
mutation CreateNewDummyItem($input: DummyItemInput!) {
  addDummyItem(input: $input){
    id
    name
    updatedAt
  }
}
`
`
{
  "input": {
    "name": "item 2",
    "updatedAt": "2021-11-27T17:00:00.000Z"
  }
}
`
## Pagination, using cursor-based
`
query Search($query: String, $include_adult: Boolean = true, $first: Int = 10, $after: Int) {
  movies(query: $query, include_adult: $include_adult, first: $first, afterCursor: $after) {
    totalCount
    edges {
      node{
        id
        title
      }
      cursor
    }
    pageInfo{
      endCursor
      hasNextPage
    }
  }
}
`
`
{
  "query": "spider man",
  "include_adult": true,
  "first": 2
}
`
`
{
  "query": "spider man",
  "include_adult": true,
  "first": 2,
  "after": 315635
}
`

## Some useful doc
https://daily.dev/blog/pagination-in-graphql


# Start
$npm start

## Debugging
### Option 1
1 Start task
Ctrl+Shift+B
>Tasks: Run task build

2 Start debug

### Option 2, attact a process to Ts-node-dev
1 Start debug
$npm run start:debug

2 F5, choose profile "Typescript server"