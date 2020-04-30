import hudson.tasks.test.AbstractTestResultAction

def PrintResult() {

    AbstractTestResultAction testResultAction = currentBuild.rawBuild.getAction(AbstractTestResultAction.class);

    if (testResultAction != null) {
        def total = testResultAction.totalCount;
        def failed = testResultAction.failCount;
        def skipped = testResultAction.skipCount;
        def passed = total - failed - skipped;

        println("TESTS - TOTAL: " + testResultAction.totalCount);
        println("TESTS - FAILED: " + testResultAction.failCount);
        println("TESTS - SKIPPED: " + testResultAction.skipCount);
        println("TESTS - PASSED: " + passed);
    }
    
}