import express from 'express';
import { ApolloServer } from 'apollo-server-express';
import { ApolloServerPluginLandingPageGraphQLPlayground } from 'apollo-server-core';
import { buildSchema, registerEnumType } from 'type-graphql';
import { MovieResolver } from './schema/movie-resolver';
import { DummyResolver } from './schema/dummy-resolver';
import { DummyStatus } from './schema/dummy-status-enum';

const app = express();

const buildApolloServer = async() => {

    // Register Enum
    registerEnumType(DummyStatus, {
        name: 'DummyStatus',
        description: 'Basic Status',
        //optional
        valuesConfig: {
            DRAFT: {
                description: 'the description of draft'
            },
            PROCESSING: {
                description: 'the descripotion of processing'
            },
            REJECT: {
                description: 'the description of reject'
            },
            DONE: {
                description: 'The done description'
            },
            DEPRECATION: {
                deprecationReason: 'this is test description for element deprecated'
            }
        }
    });

    const schema = await buildSchema({
        resolvers: [MovieResolver, DummyResolver],
        dateScalarMode: 'isoDate'

    })
    const apolloServer = new ApolloServer({
        schema,
        plugins: [ApolloServerPluginLandingPageGraphQLPlayground],
    });
    return apolloServer;
}


export {app, buildApolloServer};
