import { Int, ObjectType } from "type-graphql";
import { BaseEdge, BasePageInfo, BasePaginatedResponse } from "./base-pagination";
import { Movie } from "./movie";

@ObjectType({description: 'The MovieEdge'})
export class MovieEdge extends BaseEdge(Movie) {}

@ObjectType({description: 'The MoviePageInfo'})
export class MoviePageInfo extends BasePageInfo(Int) {}

@ObjectType({ description: 'The MoviePaginatedResponse'})
export class MoviePaginatedResponse extends BasePaginatedResponse(MovieEdge, MoviePageInfo) {}
