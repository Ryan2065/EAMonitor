CREATE OR ALTER VIEW v_EAMonitor AS
SELECT
    eaM.Id,
    eaM.Name,
    eaM.Description,
    eaM.LastModified,
    eaM.Created,
    eaMs.Name as "MonitorState",
    eaJobOuter.Created as "LastJobCreatedAt",
    eaJobOuter.LastModified as "LastJobModifiedAt",
    eaJobOuter.Completed as "LastJobCompletedAt",
    eaMJS.Name as "LastJobStatus"
FROM EAMonitor eaM
OUTER APPLY (
    SELECT TOP 1 *
    FROM EAMonitorJob eaJob
    WHERE eaJob.MonitorId = eaM.Id
    ORDER BY eaJob.Created DESC
) as eaJobOuter
JOIN EAMonitorState eaMs
    ON eaMs.Id = eaM.MonitorStateId
JOIN EAMonitorJobStatus eaMJS
    ON eaMJS.Id = eaJobOuter.JobStatusId