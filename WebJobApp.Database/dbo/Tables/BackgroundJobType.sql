-- Create Seed Data Table...

CREATE TABLE [dbo].[BackgroundJobType] (
    [BackgroundJobTypeID]           INT                 NOT NULL,
    [Name]                          VARCHAR(255)        NOT NULL,
    [HandlerAssemblyName]           VARCHAR(255)        NOT NULL,
    [HandlerClassName]              VARCHAR(255)        NOT NULL,
    [MaximumRetryCount]             INT                 NULL,
    [NextAttemptDelayInMS]          INT                 NULL,
    [Category]                      VARCHAR(100)        NULL,
    [SuspendFlag]                   BIT                 NOT NULL DEFAULT(0),

    CONSTRAINT [PK_BackgroundJobType]
        PRIMARY KEY CLUSTERED (
            [BackgroundJobTypeID] ASC
        )
        WITH (FILLFACTOR = 100)
);
GO

-- Create Indexes...
