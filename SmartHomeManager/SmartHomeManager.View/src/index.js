import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { ChakraProvider } from '@chakra-ui/react';
import theme from './theme'

const root = ReactDOM.createRoot(document.getElementById('root'));

// Removed React.StrictNode, prevent API calls from calling twice
// https://stackoverflow.com/questions/73002902/api-getting-called-twice-in-react

root.render(
    <ChakraProvider theme={theme}>
        {/* <React.StrictMode> */}
            <App />
        {/* </React.StrictMode> */}
    </ChakraProvider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
