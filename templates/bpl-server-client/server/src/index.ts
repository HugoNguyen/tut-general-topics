// This is a minimalist web-framework for NodeJS
const express = require('express');

// This is enable cors
const cors = require('cors')

// This is parse middleware, that helps parsing incoming requests' body
const bodyParser = require('body-parser');

// Cookie middleware
const cookieParser = require('cookie-parser');

// Handle session
const session = require('express-session');

// Log useful info in commandline about request
const morgan = require('morgan');

const path = require('path');

const publicFolderPath = path.join(__dirname, '../public');

// Init express
const app = express();

// Enable All CORS Request
app.use(cors({
    origin: '*'
}));

// Set the initial port of backend app
app.set('port', 80);

// In !Development! mode we can set to log the request
app.use(morgan('dev'));

// body.parser init, it will parse the incoming parameters into req body
app.use(bodyParser.urlencoded({extended: true}));

app.use(function(req: any, res: any, next: any) {
    res.header('Access-Control-Allow-Origin', "http://localhost:3000");
    res.header(
        'Access-Control-Allow-Headers',
        'Origin, X-Requested-With, Content-Type, Accept'
    );
    next();
});

var handleFn = (req: any, res: any, next: any) => {
    next();
};

// Init route
app.get('/', handleFn, (req: any, res: any) => {
    res.redirect('/api');
});

// Api route
app.route('/api').get(handleFn, (req: any, res: any) => {
    res.sendFile( publicFolderPath + '/api.html');
});

// Car list
app.route('/api/cars').get(handleFn, (req: any, res: any) => {
    debugger;
    res.sendFile(publicFolderPath + '/cars.json');
});

// test
app.route('/api/test').get(handleFn, (req: any, res: any) => {
    res.status(200).send('Black Pink 2');
});

// Route for handling 404
app.use(function(req: any, res: any, next: any) {
    res.status(404).send('Sorry can\'t find that!');
});

// 500 error
function clientErrorHandler(err: any, req: any, res: any, next: any) {
    if (req.xhr) {
        res.status(500).send({error: 'Somthing failded!'});
    } else {
        next(err);
    }
}

app.use(clientErrorHandler);

// start server
app.listen(app.get('port'), () => console.log(`App started on port ${app.get('port')}`));
