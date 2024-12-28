import { NODE_ENV } from '$env/static/private';

let env = NODE_ENV || 'development';

// toggle this when debugging production build
// env = 'preview';

export let serverOptions = {
    baseUrl: env === 'development' ? 'http://localhost:5050'
        : env === 'production' ? ''
            : 'http://localhost:5000',
    apiUrl: env === 'development' ? 'http://localhost:5050/api'
        : env === 'production' ? ''
            : 'http://localhost:5000/api'
};