import User from '../models/user';
import cloudinary from 'cloudinary';
import ErrorHandler from '../utils/errorHandler';
import catchAsyncErrors from '../middlewares/catchAsyncError';

// Setting up cloudinary config
cloudinary.config({
    cloud_name: process.env.CLOUDINARY_CLOUD_NAME,
    api_key: process.env.CLOUDINARY_API_KEY,
    api_secret: process.env.CLOUDINARY_SECRET_KEY,
});

// Register user => /api/auth/register
const registerUser = catchAsyncErrors(async (req, res, next) => {

    if (!req.body.avatar) {
        return next(new ErrorHandler('Avatar missing', 400));
    }

    const result = await cloudinary.v2.uploader.upload(req.body.avatar, {
        folder: 'bookit/avatar',
        width: '150',
        crop: 'scale',
    });
    
    const { name, email, password } = req.body;

    const user = await User.create({
        name,
        email,
        password,
        avatar: {
            public_id: result.public_id,
            url: result.secure_url
        }
    });

    res.status(200).json({
        success: true,
        message: 'Account Registered successfully',
    })
});

// Cuurent user profile   =>   /api/me
const currentUserProfile = catchAsyncErrors(async (req, res) => {

    const user = await User.findById(req.user._id);

    res.status(200).json({
        success: true,
        user
    })

})

export {
    registerUser,
    currentUserProfile
}