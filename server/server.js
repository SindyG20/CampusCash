require('dotenv').config();
const express = require('express');
const cors = require('cors');

const authRoutes = require('./routes-auth');

const app = express();

app.use(cors());
app.use(express.json());

app.get('/', (req, res) => res.send('CampusCash API is running'));
app.use('/api/auth', authRoutes);

const port = process.env.PORT || 3000;
app.listen(port, () => console.log(`API running on http://localhost:${port}`));