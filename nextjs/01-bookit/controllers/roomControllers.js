import Room from '../models/rooms';
import ErrorHandler from '../utils/errorHandler';
import catchAsyncErrors from '../middlewares/catchAsyncError';
import APIFeatures from '../utils/apiFeatures';

const allRooms = catchAsyncErrors(async (req, res) => {
    const resPerPage = 4;
    const roomCount = await Room.countDocuments();
    
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
        roomCount,
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

export {
    allRooms,
    newRoom,
    getSingleRoom,
    updateRoom,
    deleteRoom,
}