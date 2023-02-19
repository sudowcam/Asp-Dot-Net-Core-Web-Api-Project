using System;

namespace API.Services.Test
{
    public class UnitTest
    {
        [Fact]
        public void ShouldReturnListOfNotes()
        {
            // Arrange
            var processor = new RequestNoteProcessor();
            var request = new RequestNoteList{};

            // Act
            ResponseNote response = processor.GetNoteList(request);

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public void ShouldReturnNoteDetails()
        {
            // Arrange
            var processor = new RequestNoteProcessor();
            var request = new RequestNoteDetails
            {
                NoteId = 1
            };

            // Act
            ResponseNote response = processor.GetNoteDetails(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.NoteId, response.NoteId);
        }

        [Fact]
        public void ShouldThrowWhenNoteIdIsNull()
        {
            // Arrange
            var processor = new RequestNoteProcessor();

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => processor.GetNoteDetails(null));

            // Assert
            Assert.Equal("request", exception.ParamName);
        }
    }
}