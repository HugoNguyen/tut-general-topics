# Server

$npm i express reflect-metadata class-validator axios
$npm i graphql type-graphql apollo-server-express apollo-server-core

$npm install @types/express @types/node ts-node-dev typescript --save-dev



# Query
## Search
`
query Search($query: String!, $include_adult: Boolean = true, $first: Int = 10, $after: Int) {
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
  "first": 10
}
`