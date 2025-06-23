using TaskManager.Domain.Enumerators;
using TaskManager.Domain.ValueObjects;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.Domain.Tests.Entities.Tests;

public class TaskTests
{
    [Fact]
    public void ConstructorShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var title = new Title("New Task");
        var description = new Description("Task description");
        const string createdById = "user123";

        // Act
        var task = new Task(title, description, createdById);

        // Assert
        Assert.Equal(title, task.Title);
        Assert.Equal(description, task.Description);
        Assert.Equal(Status.Pending, task.Status);
        Assert.Equal(createdById, task.CreatedById);
        Assert.Null(task.CompletedAt);
        Assert.True(task.CreatedAt <= DateTime.UtcNow);
        Assert.Null(task.UpdatedAt);
    }

    [Fact]
    public void ConstructorShouldThrowArgumentNullExceptionWhenTitleIsNull()
    {
        // Arrange
        var description = new Description("desc");
        const string createdById = "user123";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Task(null, description, createdById));
    }

    [Fact]
    public void ConstructorShouldSetEmptyDescriptionWhenDescriptionIsNull()
    {
        // Arrange
        var title = new Title("Title");
        const string createdById = "user123";

        // Act
        var task = new Task(title, null, createdById);

        // Assert
        Assert.NotNull(task.Description);
        Assert.Equal(string.Empty, task.Description.Value);
    }

    [Fact]
    public void ConstructorShouldThrowArgumentNullExceptionWhenCreatedByIdIsNull()
    {
        // Arrange
        var title = new Title("Title");
        var description = new Description("desc");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Task(title, description, null));
    }

    [Fact]
    public void UpdateTitleShouldChangeTitleAndUpdateUpdatedAt()
    {
        // Arrange
        var task = new Task(new Title("Old Title"), new Description("desc"), "user123");
        var newTitle = new Title("New Title");

        // Act
        task.UpdateTitle(newTitle);

        // Assert
        Assert.Equal(newTitle, task.Title);
        Assert.NotNull(task.UpdatedAt);
        Assert.True(task.UpdatedAt > task.CreatedAt);
    }

    [Fact]
    public void UpdateTitleShouldNotChangeWhenNewTitleIsEqual()
    {
        // Arrange
        var title = new Title("Same Title");
        var task = new Task(title, new Description("desc"), "user123");

        // Act
        task.UpdateTitle(title);

        // Assert
        Assert.Equal(title, task.Title);
        Assert.Null(task.UpdatedAt);
    }

    [Fact]
    public void UpdateTitleShouldThrowArgumentNullExceptionWhenNewTitleIsNull()
    {
        // Arrange
        var task = new Task(new Title("Title"), new Description("desc"), "user123");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => task.UpdateTitle(null));
    }

    [Fact]
    public void UpdateDescriptionShouldChangeDescriptionAndUpdateUpdatedAt()
    {
        // Arrange
        var task = new Task(new Title("Title"), new Description("Old desc"), "user123");
        var newDescription = new Description("New desc");

        // Act
        task.UpdateDescription(newDescription);

        // Assert
        Assert.Equal(newDescription, task.Description);
        Assert.NotNull(task.UpdatedAt);
        Assert.True(task.UpdatedAt > task.CreatedAt);
    }

    [Fact]
    public void UpdateDescriptionShouldSetEmptyDescriptionWhenNewDescriptionIsNull()
    {
        // Arrange
        var task = new Task(new Title("Title"), new Description("Old desc"), "user123");

        // Act
        task.UpdateDescription(null);

        // Assert
        Assert.NotNull(task.Description);
        Assert.Equal(string.Empty, task.Description.Value);
        Assert.NotNull(task.UpdatedAt);
    }

    [Fact]
    public void UpdateDescriptionShouldNotChangeWhenNewDescriptionIsEqual()
    {
        // Arrange
        var description = new Description("Same desc");
        var task = new Task(new Title("Title"), description, "user123");

        // Act
        task.UpdateDescription(description);

        // Assert
        Assert.Equal(description, task.Description);
        Assert.Null(task.UpdatedAt);
    }
}
