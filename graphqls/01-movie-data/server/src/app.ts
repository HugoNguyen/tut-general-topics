import express from 'express';
import { graphqlHTTP } from 'express-graphql';
import schema from './schema/schema';

const app = express();

app.use('/graphql', graphqlHTTP({
    schema,
    graphiql: true, // optionally enable GraphiQL mode
}));

app.get("/users", (_, response) => {
    console.log('test');

    response.send({ a: 'a' })
});

export {app};
