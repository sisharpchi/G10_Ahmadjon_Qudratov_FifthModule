Create database ToDoItemDB;

CREATE TABLE ToDoItems (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000) NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
    DueDate DATETIME NOT NULL
);


go
CREATE PROCEDURE sp_AddToDoItem
    @Title NVARCHAR(200),
    @Description NVARCHAR(1000),
    @IsCompleted BIT,
    @CreatedAt DATETIME,
    @DueDate DATETIME
AS
BEGIN
    INSERT INTO ToDoItems (Title, Description, IsCompleted, CreatedAt, DueDate)
	OUTPUT INSERTED.Id
    VALUES (@Title, @Description, @IsCompleted, @CreatedAt, @DueDate)
END

go
CREATE PROCEDURE sp_DeleteToDoItemById
    @Id BIGINT
AS
BEGIN
    DELETE FROM ToDoItems
    WHERE Id = @Id;
END

go
CREATE PROCEDURE sp_UpdateToDoItem
    @Id BIGINT,
    @Title NVARCHAR(200),
    @Description NVARCHAR(1000),
    @IsCompleted BIT,
    @DueDate DATETIME
AS
BEGIN
    UPDATE ToDoItems
    SET Title = @Title,
        Description = @Description,
        IsCompleted = @IsCompleted,
        DueDate = @DueDate
    WHERE Id = @Id
END


go
CREATE PROCEDURE sp_GetAllToDoItems
    @Skip INT,
    @Take INT
AS
BEGIN
    SELECT * FROM ToDoItems
    ORDER BY CreatedAt DESC
    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;
END

go
CREATE PROCEDURE sp_GetToDoItemById
    @Id BIGINT
AS
BEGIN
    SELECT * FROM ToDoItems WHERE Id = @Id
END

go
CREATE PROCEDURE sp_GetByDueDate
    @DueDate DATE
AS
BEGIN
    SELECT * FROM ToDoItems WHERE CAST(DueDate AS DATE) = @DueDate
END

go
CREATE PROCEDURE sp_GetCompletedItems
    @Skip INT,
    @Take INT
AS
BEGIN
    SELECT * FROM ToDoItems
    WHERE IsCompleted = 1
    ORDER BY CreatedAt DESC
    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;
END

go
CREATE PROCEDURE sp_GetIncompleteItems
    @Skip INT,
    @Take INT
AS
BEGIN
    SELECT * FROM ToDoItems
    WHERE IsCompleted = 0
    ORDER BY CreatedAt DESC
    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;
END


go
CREATE FUNCTION CountOfToDoLists()
RETURNS INT
AS
BEGIN
    DECLARE @counts INT;

    SELECT @counts = COUNT(*) FROM ToDoItems;

    RETURN @counts;
END;