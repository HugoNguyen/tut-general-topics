import { Arg, FieldResolver, Int, Query, Resolver, Root } from "type-graphql";
import { Movie } from "./movie";
import theMovieInstance from "../the-movie-service";
import { Cast, Crew } from "./person";
import { MoviePaginatedResponse } from "./movie-search-result";

@Resolver(of => Movie)
export class MovieResolver {

    @Query(() => Movie)
    async movie(@Arg("id") id: string) {
        // code to get data from db/other source
        try {
            const { status, data } = await theMovieInstance.get(`/movie/${id}?language=en-US`);
            if (status === 200) {
                return data;
            }
        } catch (errr) {
            throw new Error('The resource you requested could not be found.');
        }

    }

    @Query(() => MoviePaginatedResponse)
    async movies(
        @Arg('query') query: string,
        @Arg('include_adult') include_adult: boolean,
        @Arg('first', ()=> Int) first: number,
        @Arg('afterCursor', () => Int, { nullable: true }) afterCursor: number,
    ) {
        let page = 1;
        let slicedData: any[] = [];
        let foundAfterCursor = !afterCursor;
        let hasNextPage = true;
        let totalCount: number = 0;

        while (slicedData.length < first && hasNextPage) {
            const queryBuilder = `/search/movie?language=en-US&query=${encodeURI(query)}&page=${page ?? 1}&include_adult=${include_adult ?? false}`;
            const { data }: {
                data: {
                    page: number,
                    results: any[],
                    total_pages: number,
                    total_results: number,
                }
            } = await theMovieInstance.get(queryBuilder);


            if (!foundAfterCursor) {
                const nodeIndex = data.results.findIndex(el => el.id === +afterCursor);

                if (nodeIndex > 0) {
                    slicedData = [...slicedData, ...data.results.slice(nodeIndex + 1, nodeIndex + 1 + first)];
                    foundAfterCursor = true;
                }
            } else {
                slicedData = [...slicedData, ...data.results.slice(0, first - slicedData.length)];
            }

            totalCount = data.total_results;
            hasNextPage = data.total_pages !== page;
            page++;
        }

        return {
            totalCount,
            edges: slicedData.map(node => ({
                node,
                cursor: node.id,
            })),
            pageInfo: {
                endCursor: slicedData.length > 0 ? slicedData[slicedData.length - 1].id : null,
                hasNextPage
            }
        }
    }

    @FieldResolver()
    async director(@Root() movie: Movie): Promise<Crew | undefined> {
        const { data: { crew } } = await theMovieInstance.get(`/movie/${movie.id}/credits?language=en-US`);
        if (crew && Array.isArray(crew)) {
            return crew.filter(el => el.job === 'Director')[0];
        }

        return undefined;
    }

    @FieldResolver()
    async casts(@Root() movie: Movie): Promise<Cast[]> {
        const { data: { cast } } = await theMovieInstance.get(`/movie/${movie.id}/credits?language=en-US`);
        return cast;
    }
}