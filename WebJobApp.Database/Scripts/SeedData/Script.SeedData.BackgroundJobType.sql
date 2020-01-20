-- Script.SeedData.BackgroundJobType.sql

--
-- NOTE:
-- BackgroundJob 140 is the last of the original BackgroundRequestType's from CT.
-- New CTWeb Background Jobs start with 141.
--

--
-- Please maintain the column formatting.
--

MERGE INTO dbo.BackgroundJobType AS tgt
USING (VALUES
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--  BackgroundJobTypeID                                         HandlerAssemblyName                                                                                 MaximumRetryCount               Category
--          Name                                                                                                    HandlerClassName                                        NextAttemptDelayInMS
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
(   1,     'SendEmail',                                        'IEX.LDC.EnergyTrading.JobAdaptor',                 'IEX.LDC.EnergyTrading.JobAdaptor.SendEamail',                                    4,         300000,             NULL),
(   99999,  'TestJob',                                          'WebJobApp',                                       'WebJobApp.TestJob',                                     10,         300000,             'Test Job')
)

AS src (BackgroundJobTypeID, Name, HandlerAssemblyName, HandlerClassName, MaximumRetryCount, NextAttemptDelayInMS, Category)
ON
    tgt.BackgroundJobTypeID = src.BackgroundJobTypeID
WHEN MATCHED THEN
    UPDATE SET
        Name = src.[Name],
        HandlerAssemblyName = src.HandlerAssemblyName,
        HandlerClassName = src.HandlerClassName,
        MaximumRetryCount = src.MaximumRetryCount,
        NextAttemptDelayInMS = src.NextAttemptDelayInMS,
        Category = src.Category
WHEN NOT MATCHED BY TARGET THEN
    INSERT (BackgroundJobTypeID, Name, HandlerAssemblyName, HandlerClassName, MaximumRetryCount, NextAttemptDelayInMS, Category)
    VALUES (BackgroundJobTypeID, Name, HandlerAssemblyName, HandlerClassName, MaximumRetryCount, NextAttemptDelayInMS, Category);
--
-- DO NOT DELETE!!!
--

