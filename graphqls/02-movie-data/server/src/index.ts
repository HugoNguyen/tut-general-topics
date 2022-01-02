import 'reflect-metadata';
import { buildApolloServer , app } from './app';

const start = async () => {
    const apolloServer = await buildApolloServer();
    await apolloServer.start();
    apolloServer.applyMiddleware({ app });
    app.listen(3000, () => {
        console.log('Listening on port 3000!!!!!!!');
    })
}

start().catch(error => {
    console.log(error, 'error');
});