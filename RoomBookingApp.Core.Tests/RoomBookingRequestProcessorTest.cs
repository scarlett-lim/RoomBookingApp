using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        private RoomBookingRequestProcessor _processor;
        private RoomBookingRequest _bookingRequest;
        private List<Room> _availableRooms;
        private Mock<IRoomBookingService> _roomBookingServiceMock;

        public RoomBookingRequestProcessorTest()
        {
            _bookingRequest = new RoomBookingRequest
            {
                FullName = "Test",
                Email = "abc@request.com",
                Date = new DateTime(2024, 10, 13)
            };

            _availableRooms = new List<Room>() { new Room() { Id = 1 } };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();

            _roomBookingServiceMock.Setup(q => q.GetAvailableRoom(_bookingRequest.Date))
                .Returns(_availableRooms);

            //inject mock service to RoomBookingRequestProcessor
            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);

        }

        [Fact]
        public void BookRoom_WithValidBookingRequest_ReturnRoomBookingRequest()
        {
            //Act
            RoomBookingResult result = _processor.BookRoom(_bookingRequest);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_bookingRequest.FullName, result.FullName);
            Assert.Equal(_bookingRequest.Email, result.Email);
            Assert.Equal(_bookingRequest.Date, result.Date);


            result.ShouldNotBeNull();
            result.FullName.ShouldBe(_bookingRequest.FullName);
            result.Email.ShouldBe(_bookingRequest.Email);
            result.Date.ShouldBe(_bookingRequest.Date);
        }

        [Fact]
        public void BookRoom_NullBookingRequest_ThrowException()
        {
            //Cannot use this because it will not able to return the result
            //RoomBookingResult result = processor.BookRoom(null);

            //Assert
            //need to pass in lambda function so that the flow can be handled by xx.throw
            //it will able to defer the execution of BookRoom instead of execute it immediately
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));
            exception.ParamName.ShouldBe("bookingRequest");

            Assert.Throws<ArgumentNullException>(() => _processor.BookRoom(null));

        }

        [Fact]
        public void BookRoom_ValidBookingRequest_ShouldSaveRoomBookingRequest()
        {
            RoomBooking savedBooking = null;
                 
            //step 1: capture any RoomBooking object passed to 'save' method
            _roomBookingServiceMock.Setup(q => q.Save(It.IsAny<RoomBooking>()))
                //step 3: when Save is called within BookRoom, the callback saves the RoomBooking object to savedBooking for later inspection.
                //booking = actual RoomBooking object passed into Save
                .Callback<RoomBooking>(booking =>
                {
                    savedBooking = booking;
                });

            //step 2
            _processor.BookRoom(_bookingRequest);

            //step 4: To verify ''save' method been called once only
            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Once);

            savedBooking.ShouldNotBeNull();
            savedBooking.FullName.ShouldBe(_bookingRequest.FullName);
            savedBooking.Email.ShouldBe(_bookingRequest.Email);
            savedBooking.Date.ShouldBe(_bookingRequest.Date);
            savedBooking.RoomId.ShouldBe(_availableRooms.First().Id);
        }

        [Fact]
        public void BookRoom_WithoutAvailableRoom_ShouldNotSaveRoomBookingRequestIfNoneAvailable()
        {
            //simulate no available room
            _availableRooms.Clear();

            _processor.BookRoom(_bookingRequest);

            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Never);

        }
    }
}
