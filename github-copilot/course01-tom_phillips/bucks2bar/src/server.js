const express = require('express');
const nodemailer = require('nodemailer');
const app = express();
app.use(express.json());

app.post('/send-chart-email', async (req, res) => {
  const { email, image } = req.body;
  if (!email || !image) return res.status(400).json({ message: 'Missing data' });

  // Configure transporter (use your SMTP server)
  let transporter = nodemailer.createTransport({
    host: 'smtp.example.com', // Use your SMTP server
    port: 587,
    secure: false,
    auth: {
      user: 'your@email.com',
      pass: 'yourpassword'
    }
  });

  // Send email
  try {
    await transporter.sendMail({
      from: '"Bucks2Bar" <your@email.com>',
      to: email,
      subject: 'Your Bucks2Bar Chart',
      text: 'See attached chart image.',
      attachments: [{
        filename: 'chart.png',
        content: image.split("base64,")[1],
        encoding: 'base64'
      }]
    });
    res.json({ message: 'Email sent!' });
  } catch (err) {
    res.status(500).json({ message: 'Failed to send email' });
  }
});

app.listen(3000, () => console.log('Server running on port 3000'));