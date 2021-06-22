const http = axios.create({
    baseURL: process.env.REACT_APP_API_URL,
    transformRequest: [
      function(data, headers = {}) {
        if (Boolean(getCookie('token')) && getCookie('token') !== 'undefined') {
          headers['Authorization'] = `Bearer ${getCookie('token')}`;
        }

        if (headers['Content-Type'] !== 'multipart/form-data') {
          headers['Content-Type'] = 'application/json';
          if (typeof data !== 'string') {
            data = JSON.stringify(data);
          }
        }
        if (!headers['AccessToken']) {
          delete headers['AccessToken'];
        }
        if (!headers['RefreshToken']) {
          delete headers['RefreshToken'];
        }

        return data;
      },
    ],
  });
