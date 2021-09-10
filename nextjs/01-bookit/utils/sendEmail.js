import nodemailer from 'nodemailer';
import getConfig from "next/config";

const sendEmail = async options => {

    // Config
    const { serverRuntimeConfig: { SMTP_HOST, SMTP_PORT, SMTP_USER, SMTP_PASSWORD, SMTP_FROM_NAME, SMTP_FROM_EMAIL } } = getConfig();

    var transport = nodemailer.createTransport({
        host: SMTP_HOST,
        port: SMTP_PORT,
        auth: {
            user: SMTP_USER,
            pass: SMTP_PASSWORD
        }
    });

    const message = {
        from: `${SMTP_FROM_NAME} <${SMTP_FROM_EMAIL}>`,
        to: options.email,
        subject: options.subject,
        text: options.message
    }

    await transport.sendMail(message);
}

export default sendEmail;