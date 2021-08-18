import axios from "axios";

const LandingPage = ({ currentUser }) => {
  console.log(currentUser);
  return <h1>Landing Page</h1>;
};

LandingPage.getInitialProps = async({ req }) => {
  console.log(req.headers);
  if (typeof window === 'undefined') {
    // we are on the server!
    // request should be made to http://ingress-nginx-controller.ingress-nginx.....
    const { data } = await axios.get(
      'http://ingress-nginx-controller.ingress-nginx.svc.cluster.local/api/users/currentuser',
      {
        headers: {
          // Host: 'ticketing.hugo2908-02-ticking-auth.com' // get from headers sent from browser
          ...req.headers
        }
      }
    );

    return data;
  } else {
    // we are on the browser!
    // requests can be made with a base url of ''
    const { data } = await axios.get('/api/users/currentuser');

    return data;
  }
  
  return {};
}

export default LandingPage;
