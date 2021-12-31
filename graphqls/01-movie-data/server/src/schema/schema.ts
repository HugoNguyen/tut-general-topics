import {
    GraphQLObjectType,
    GraphQLString,
    GraphQLID,
    GraphQLBoolean,
    GraphQLInt,
    GraphQLList,
    GraphQLNonNull,
    GraphQLSchema,
    GraphQLInterfaceType,
    GraphQLInputObjectType,
    GraphQLScalarType,
    Kind,
    GraphQLEnumType
} from 'graphql';
import axios from 'axios';

const apiKey = '7ef4a8b8370f25f7702f8cbce6825e7b';

const dummyData: { id: number, name: string, updatedAt: Date }[] = [];

const theMovieInstance = axios.create({
    baseURL: `https://api.themoviedb.org/3`
});

theMovieInstance.interceptors.request.use(config => {
    config.url += `&api_key=${apiKey}`;
    console.log(config.url);
    return config;
})

const MovieType = new GraphQLObjectType({
    name: 'Movie',
    fields: () => ({
        id: { type: GraphQLID },
        imdb_id: { type: GraphQLString },
        title: { type: GraphQLString },
        overview: { type: GraphQLString },
        director: {
            type: CrewType,
            async resolve(source, args) {
                const parent = source; // movie data;
                const { data: { crew } } = await theMovieInstance.get(`/movie/${parent.id}/credits?language=en-US`);
                if (crew && Array.isArray(crew)) {
                    return crew.filter(el => el.job === 'Director')[0];
                }

                return undefined;
            }
        },
        casts: {
            type: new GraphQLList(CastType),
            async resolve(parent) {
                const { data: { cast } } = await theMovieInstance.get(`/movie/${parent.id}/credits?language=en-US`);
                return cast;
            }
        }
    })
});

const IPeopleType = new GraphQLInterfaceType({
    name: 'IPeople',
    fields: {
        id: { type: GraphQLID },
        adult: { type: GraphQLBoolean },
        gender: { type: GraphQLInt },
        known_for_department: { type: GraphQLString },
        name: { type: GraphQLString },
        original_name: { type: GraphQLString },
        popularity: { type: GraphQLInt },
        profile_path: { type: GraphQLString },
        credit_id: { type: GraphQLString },
    }
});

const CastType = new GraphQLObjectType({
    name: 'Cast',
    interfaces: [IPeopleType],
    fields: () => ({
        id: { type: GraphQLID },
        adult: { type: GraphQLBoolean },
        gender: { type: GraphQLInt },
        known_for_department: { type: GraphQLString },
        name: { type: GraphQLString },
        original_name: { type: GraphQLString },
        popularity: { type: GraphQLInt },
        profile_path: { type: GraphQLString },
        credit_id: { type: GraphQLString },

        cast_id: { type: GraphQLInt },
        character: { type: GraphQLString },
        order: { type: GraphQLInt },
    })
});

const CrewType = new GraphQLObjectType({
    name: 'Crew',
    interfaces: [IPeopleType],
    fields: () => ({
        id: { type: GraphQLID },
        adult: { type: GraphQLBoolean },
        gender: { type: GraphQLInt },
        known_for_department: { type: GraphQLString },
        name: { type: GraphQLString },
        original_name: { type: GraphQLString },
        popularity: { type: GraphQLInt },
        profile_path: { type: GraphQLString },
        credit_id: { type: GraphQLString },

        department: { type: GraphQLString },
        job: { type: GraphQLString },
    })
});

const EdgeType = new GraphQLObjectType({
    name: 'Edge',
    fields: {
        node: { type: MovieType },
        cursor: { type: GraphQLInt },
    }
});

const PageInfoType = new GraphQLObjectType({
    name: 'PageInfo',
    fields: {
        endCursor: { type: GraphQLInt },
        hasNextPage: { type: GraphQLBoolean },
    }
});

const SearchResultType = new GraphQLObjectType({
    name: 'SearchResult',
    fields: {
        totalCount: { type: GraphQLInt },
        edges: { type: new GraphQLList(EdgeType) },
        pageInfo: { type: PageInfoType },
    }
});

const DateScalar = new GraphQLScalarType({
    name: 'Date',
    description: 'Date custom scalar type',
    serialize(value: any) {
        return value.getTime(); // Convert outgoing Date to integer for JSON
    },
    parseValue(value: any) {
        return new Date(value); // Convert incoming integer to Date
    },
    parseLiteral(ast) {
        if (ast.kind === Kind.INT) {
            return new Date(parseInt(ast.value, 10)); // Convert hard-coded AST string to integer and then to Date
        }
        return null; // Invalid hard-coded value (not an integer)
    },
})

const DateOutputEnumType = new GraphQLEnumType({
    name: 'DateOutputEnum',
    values: {
        TIMESPAN: {
            value: 0,
        },
        ISO: {
            value: 1,
        },
    },
});

const DummyType = new GraphQLObjectType({
    name: 'Dummy',
    fields: {
        id: { type: GraphQLID },
        name: { type: GraphQLString },
        updatedAt: {
            type: new GraphQLNonNull(DateScalar),
            args: {
                output: { type: DateOutputEnumType },
            },
            resolve(parent, { output }) {
                console.log(parent);
                return parent.updatedAt;
            }
        },
    }
});

const DummyItemInput = new GraphQLInputObjectType({
    name: 'DummyItemInput',
    fields: {
        name: { type: new GraphQLNonNull(GraphQLString) },
        updatedAt: { type: DateScalar },
    }
});

const RootQuery = new GraphQLObjectType({
    name: 'RootQueryType',
    fields: {
        movie: {
            type: MovieType,
            args: { id: { type: GraphQLID } },
            async resolve(source, args) {
                // code to get data from db/other source
                const { id } = args;
                const { data } = await theMovieInstance.get(`/movie/${id}?language=en-US`);

                return data;
            }
        },
        movies: {
            type: SearchResultType,
            args: {
                query: { type: GraphQLString },
                include_adult: { type: GraphQLBoolean },
                first: { type: GraphQLInt },
                afterCursor: { type: GraphQLInt },
            },
            async resolve(source, args) {
                const { query, include_adult, first, afterCursor } = args;
                // try to find item matching with afterCursor
                let page = 1;
                let slicedData: any[] = [];
                let foundAfterCursor = !afterCursor;
                let hasNextPage = true;
                let totalCount: number = 0;

                while(slicedData.length < first && hasNextPage) {
                    const queryBuilder = `/search/movie?language=en-US&query=${encodeURI(query)}&page=${page ?? 1}&include_adult=${include_adult ?? false}`;
                    const { data }: { data: {
                        page: number,
                        results: any[],
                        total_pages: number,
                        total_results: number,
                    }} = await theMovieInstance.get(queryBuilder);

                    
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
                        endCursor: slicedData.length > 0 ? slicedData[slicedData.length-1].id : null,
                        hasNextPage
                    }
                }
            }
        },
        dummyItems: {
            type: new GraphQLList(DummyType),
            resolve(source, args) {
                return dummyData;
            }
        }
    }
});

const Mutation = new GraphQLObjectType({
    name: 'Mutation',
    fields: {
        addDummyItem: {
            type: DummyType,
            args: {
                input: {
                    type: new GraphQLNonNull(DummyItemInput)
                }
            },
            resolve(source, args) {
                const { input: { name, updatedAt } } = args;
                if (!updatedAt || !(updatedAt instanceof Date) || isNaN(updatedAt.getTime())) {
                    throw new Error("updatedAt invalid");
                }
                // TODO: need add validate when updateAt is not a Date object
                const lastItem = dummyData.sort((a, b) => b.id - a.id)[0];
                const newItem = {
                    id: lastItem ? lastItem.id + 1 : 1,
                    name,
                    updatedAt: updatedAt ? updatedAt : new Date()
                };
                dummyData.push(newItem);
                return newItem;
            }
        },
    }
})

const schema = new GraphQLSchema({
    query: RootQuery,
    mutation: Mutation,
});

export default schema;