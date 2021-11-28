# Project

The project acts as a front end for Construction Equipment Rental backend API. It uses ReactJs application created with `create-react-app`. 

Application has basic features required to support system functionality.

* It displays datagrid with available rental equipment
* Allows user to select one or multiple equipment items and enter desired number of days for rental
* Sends rental request to the server and as a response gives user ability to download `PDF` invoice with prices and bonus points included.

**Note**: There is no separate orders page, so invoices need to be generated right after the order is made.

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

**Note**: In order to make successful requests, backend service needs to be running on port 5000.

### `npm test`

Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### Docker

To run application inside docker, build the image with the Dockerfile located in this directory and run.

**Note**: In order to make successful requests, backend service needs to be running on port 5000, so it's better to use `docker-compose` file located in the root directory instead.