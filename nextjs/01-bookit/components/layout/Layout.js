import React from "react";
import Head from "next/head";
import Header from "./Header";
import Footer from "./Footer";

const Layout = ({ children, title='Book Best Hotels for your Holiday' }) => {
    return (
        <div>
            <Head>
                <title>{title}</title>
                <meta charSet="UTF-8" />
                <meta httpEquiv="X-UA-Compatible" content="IE=edge" />
                <meta name="viewport" content="width=device-width, initial-scale=1.0" />
            </Head>

            <Header />
            {children}
            <Footer />
        </div>
    );
}

export default Layout;
