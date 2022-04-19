CREATE VIEW v_EAMonitor AS
SELECT
    eaM.Id,
    eaM.Name,
    eaM.Description,
    eaM.LastModified,
    eaM.Created,
    eaMs.Name as "MonitorState",
    eaJob.Created as "LastJobCreatedAt",
    eaJob.LastModified as "LastJobModifiedAt",
    eaJob.Completed as "LastJobCompletedAt",
    eaMJS.Name as "LastJobStatus"
FROM EAMonitor eaM
LEFT JOIN EAMonitorJob eaJob
    ON eaJob.Id = (
        SELECT Id
        FROM EAMonitorJob
        WHERE MonitorId = eam.Id
        ORDER BY Created DESC
        LIMIT 1
    )
JOIN EAMonitorState eaMs
    ON eaMs.Id = eaM.MonitorStateId
JOIN EAMonitorJobStatus eaMJS
    ON eaMJS.Id = eaJob.JobStatusId