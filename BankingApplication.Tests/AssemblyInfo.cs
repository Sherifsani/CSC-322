// The repositories are singletons that share NDJSON files on disk, so tests must not run
// concurrently — parallel tests would clobber each other's data.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
