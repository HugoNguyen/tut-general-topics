import Room from '../models/room';
import Booking from '../models/booking';
import ErrorHandler from '../utils/errorHandler';
import catchAsyncErrors from '../middlewares/catchAsyncError';
import APIFeatures from '../utils/apiFeatures';

const allRooms = catchAsyncErrors(async (req, res) => {
    const resPerPage = 4;
    const roomsCount = await Room.countDocuments();
    
    let apiFeatures = new APIFeatures(Room.find(), req.query)
        .search()
        .filter();

    let rooms = await apiFeatures.query;
    const filteredRoomsCount = rooms.length;

    // Error => query.find({}) did exec
    //apiFeatures.pagination(resPerPage);
    apiFeatures = new APIFeatures(Room.find(), req.query)
        .search()
        .filter()
        .pagination(resPerPage);
    rooms = await apiFeatures.query;
    res.status(200).json({
        success: true,
        roomsCount,
        resPerPage,
        filteredRoomsCount,
        rooms
    });
});

// Create new room => /api/rooms 
const newRoom = catchAsyncErrors(async (req, res) => {

    const room = await Room.create(req.body);

    res.status(200).json({
        success: true,
        room,
    });

});

// Get room details => /api/rooms/:id
const getSingleRoom = catchAsyncErrors(async (req, res, next) => {
    const { id } = req.query;
    const room = await Room.findById(id);

    if (!room) {
        return next(new ErrorHandler('Room not found with this id', 404));
    }

    res.status(200).json({
        success: true,
        room,
    });
});

// Update room details => /api/rooms/:id
const updateRoom = catchAsyncErrors(async (req, res) => {
    const { id } = req.query;
    let room = await Room.findById(id);

    if (!room) {
        res.status(400).json({
            status: false,
            error: 'Room not found with this id',
        });
    }

    room = await Room.findByIdAndUpdate(id, req.body, {
        new: true,
        runValidators: true,
        useFindAndModify: false,
    })

    res.status(200).json({
        success: true,
        room,
    });
});

// Delete room => /api/rooms/:id
const deleteRoom = catchAsyncErrors(async (req, res) => {
    const { id } = req.query;
    const room = await Room.findById(id);

    if (!room) {
        res.status(400).json({
            status: false,
            error: 'Room not found with this id',
        });
    }

    await room.remove();

    res.status(200).json({
        success: true,
        message: 'Room is deleted'
    });

});

// Create a new review   =>   /api/reviews
const createRoomReview = catchAsyncErrors(async (req, res) => {

    const { rating, comment, roomId } = req.body;

    const review = {
        user: req.user._id,
        name: req.user.name,
        rating: Number(rating),
        comment
    }

    const room = await Room.findById(roomId);

    const isReviewed = room.reviews.find(
        r => r.user.toString() === req.user._id.toString()
    )

    if (isReviewed) {

        room.reviews.forEach(review => {
            if (review.user.toString() === req.user._id.toString()) {
                review.comment = comment;
                review.rating = rating;
            }
        })

    } else {
        room.reviews.push(review);
        room.numOfReviews = room.reviews.length;
    }

    room.ratings = room.reviews.reduce((acc, item) => item.rating + acc, 0) / room.reviews.length;

    await room.save({ validateBeforeSave: false });

    res.status(200).json({
        success: true,
    })

});

// Check Review Availability   =>   /api/reviews/check_review_availability
const checkReviewAvailability = catchAsyncErrors(async (req, res) => {

    const { roomId } = req.query;

    const bookings = await Booking.find({ user: req.user._id, room: roomId });

    let isReviewAvailable = false;
    if (bookings.length > 0) isReviewAvailable = true;


    res.status(200).json({
        success: true,
        isReviewAvailable
    })

});

export {
    allRooms,
    newRoom,
    getSingleRoom,
    updateRoom,
    deleteRoom,
    createRoomReview,
    checkReviewAvailability
}
