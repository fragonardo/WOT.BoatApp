const fs = require('fs');
const path = require('path');

const apiUrl = process.env.API_BASE_URL || 'http://localhost:5000';

const config = {
  API_BASE_URL: apiUrl
};

fs.writeFileSync(
  path.join(__dirname, 'src/assets/config.json'),
  JSON.stringify(config, null, 2)
);

console.log(`✔️ API_BASE_URL set to ${apiUrl}`);
