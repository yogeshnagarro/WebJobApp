-- Create Table...

CREATE TABLE [dbo].[BackgroundJob] (
    [BackgroundJobID]               BIGINT          NOT NULL IDENTITY(1, 1),
    [BackgroundJobTypeID]           INT             NOT NULL,
    [BackgroundJobDataKey]          VARCHAR(255)    NULL,
    [BackgroundJobData]             VARCHAR(MAX)    NULL,
    [CreationDate]                  DATETIME        NOT NULL DEFAULT(getutcdate()),
    [Attempts]                      INT             NOT NULL DEFAULT(0),
    [NextAttemptDate]               DATETIME        NOT NULL DEFAULT(getutcdate()),
    [ErrorStatusFlag]               BIT             NOT NULL DEFAULT(0),
    [LastErrorDescription]          VARCHAR(MAX)    NULL,
    [CompletedDate]                 DATETIME        NULL,
    [UpdateDate]                    DATETIME        NOT NULL,
    [ExecutedDate]                  DATETIME        NULL,
    [Priority]                      TINYINT         NOT NULL DEFAULT(0),
    [ServerId]                      NVARCHAR(255)   NULL,
    [WorkerId]                      NVARCHAR(255)   NULL,   
    [HangfireJobID]                 BIGINT          NULL, 
    [DeletedFlag]                   BIT             NOT NULL DEFAULT(0),
    [ProcessingFlag]                BIT             NOT NULL DEFAULT(0),
    [RowID]                         ROWVERSION      NOT NULL,
    
    CONSTRAINT [PK_BackgroundJob]
        PRIMARY KEY CLUSTERED (
            [BackgroundJobID] ASC
        )
        WITH (FILLFACTOR = 100),
    
    CONSTRAINT [FK_BackgroundJobType_BackgroundJob]
        FOREIGN KEY ([BackgroundJobTypeID])
        REFERENCES [dbo].[BackgroundJobType] ([BackgroundJobTypeID])
);
GO

-- Create Indexes...

CREATE NONCLUSTERED INDEX [IX_BackgroundJob_BackgroundJobTypeID_BackgroundJobDataKey_StoreID_CompletedDate]
    ON [dbo].[BackgroundJob] (
        [BackgroundJobTypeID] ASC,
        [BackgroundJobDataKey] ASC,
        [StoreID] ASC,
        [CompletedDate] ASC
    )
    INCLUDE (
        [BackgroundJobData]
    )
    WITH (FILLFACTOR = 80);
GO

CREATE NONCLUSTERED INDEX [IX_BackgroundJob_BackgroundJobTypeID_StoreID_CompletedDate]
    ON [dbo].[BackgroundJob] (
        [BackgroundJobTypeID] ASC,
        [StoreID] ASC,
        [CompletedDate] ASC
    )
    WHERE
        [StoreID] IS NOT NULL
        AND [CompletedDate] IS NULL
    WITH (FILLFACTOR = 80);
GO

CREATE NONCLUSTERED INDEX [IX_BackgroundJob_BackgroundJobTypeID]
    ON [dbo].[BackgroundJob] (
        [BackgroundJobTypeID] ASC
    )
    INCLUDE (
        [BackgroundJobDataKey],
        [BackgroundJobData],
        [StoreID],
        [CompletedDate]
    )
    WHERE
        [CompletedDate] IS NULL
        AND [ServerId] IS NOT NULL
        AND [WorkerId] IS NOT NULL
        AND [HangfireJobID] IS NOT NULL
    WITH (FILLFACTOR = 80);
GO

CREATE NONCLUSTERED INDEX [IX_BackgroundJob_CreationDate]
    ON [dbo].[BackgroundJob] (
        [CreationDate] ASC
    )
    WITH (FILLFACTOR = 80);
GO

CREATE NONCLUSTERED INDEX [IX_BackgroundJob_CompletedDate_ProcessingFlag_ExecutedDate]
    ON [dbo].[BackgroundJob] (
        [CompletedDate],
        [ProcessingFlag],
        [ExecutedDate]
    )
    WITH (FILLFACTOR = 80);
GO
