import axios from "axios";

export const buildClient = ({ req }) => {
    if (typeof window === 'undefined') {
        // We are on the server
        return axios.create({
            baseURL: 'http://ingress-nginx-controller.ingress-nginx.svc.cluster.local',
            headers: {
                // Host: 'ticketing.hugo2908-02-ticking-auth.com' // get from headers sent from browser
                ...req.headers
            }
        });
    } else {
        // We must be on the browser
        return axios.create({
            baseURL: '/'
        })
    }
}

export default buildClient;