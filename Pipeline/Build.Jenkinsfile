import hudson.tasks.test.AbstractTestResultAction

pipeline {

    agent{
        label "master";
    }

    environment {
        CI_EXECUTION = "true";
        WORKSPACE = "C:\\Files";
        TEST_RESULT = "";
    }
    
    stages {

        stage ('Git Checkout') {
            steps {
                dir ("${WORKSPACE}") {
                    git branch: 'RefactorFindElementMethod', credentialsId: 'Git', url: 'https://github.com/vgcpaulino/FarfetchTest'
                }
                
                //powershell label: '', script: 'Copy-Item -Path "C:\\Program Files (x86)\\Jenkins\\workspace\\Test\\*" -Destination "C:\\Work" -Recurse'
            }
        }

        stage ('Install .Net Core') {
            steps {
                powershell label: '', returnStatus: true, script: """
                    Set-ExecutionPolicy -ExecutionPolicy Bypass
                    Invoke-WebRequest \'https://dot.net/v1/dotnet-install.ps1\' -OutFile \'${WORKSPACE}\\dotnet-install.ps1\';
                    CD ${WORKSPACE}
                    ./dotnet-install.ps1 -InstallDir ${WORKSPACE} -Version \'3.1.101\' -ProxyUseDefaultCredentials;
                """;
            }   
        }

        stage ('Install NuGet') {
            steps {
                powershell label: '', returnStatus: true, script: """
                    Set-ExecutionPolicy -ExecutionPolicy Bypass
                    Invoke-WebRequest "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -UseBasicParsing -OutFile "${WORKSPACE}\\nuget.exe"
                """; 
            }
        }

        stage ('Restore Project') {
            steps {
                powershell label: '', returnStatus: true, script: """
                    ${WORKSPACE}\\dotnet restore ${WORKSPACE}
                """;
            }
        }

        stage ('Build Project') {
            steps {
                powershell label: '', returnStatus: true, script: """
                    ${WORKSPACE}\\dotnet build ${WORKSPACE}
                """;
            }
        }

        stage ('Build Docker Compose') {
            steps {
                powershell label: '', returnStatus: true, script: '''
                    docker stop -t 0 $(docker ps -q);
                ''';            
                powershell label: '', returnStatus: true, script: """
                    docker-compose --file ./Docker/Docker-compose-Debug.yml up -d --scale chrome=5
                """;          
                sleep(time:30,unit:"SECONDS");
                
            }
        }

        stage ('Test Project') {
            steps {
                script { 
                    TEST_RESULT = powershell(label: '', returnStatus: true, script: """
                        ${WORKSPACE}\\dotnet test ${WORKSPACE} --results-directory "${WORKSPACE}" --logger "xunit;LogFileName=TestResult.xml"
                    """);
                }
                println("TEST STATUS:" + TEST_RESULT);
            }
        }

        stage ('Publish xUnit Report') {
            steps {
                dir ("${WORKSPACE}") {
                    xunit([xUnitDotNet(deleteOutputFiles: true, failIfNotNew: false, pattern: '/*.xml', skipNoTestFiles: false, stopProcessingIfError: true)]);
                    script {
                        def failedTests = CheckForFailedTests();
                        if (failedTests == true){
                            currentBuild.result = 'FAILED';
                            assert false;
                        }
                    }
                }
            }
        }

        stage ('Stop Docker Containers') {
            steps {
                powershell label: '', returnStatus: true, script: '''
                    docker stop -t 0 $(docker ps -q);
                ''';            
            }
        } 

    }
}

def CheckForFailedTests() {

    AbstractTestResultAction testResultAction = currentBuild.rawBuild.getAction(AbstractTestResultAction.class);

    if (testResultAction != null) {
        def total = testResultAction.totalCount;
        def failed = testResultAction.failCount;
        def skipped = testResultAction.skipCount;
        def passed = total - failed - skipped;

        println("TESTS - TOTAL: " + testResultAction.totalCount);
        println("TESTS - PASSED: " + passed);
        println("TESTS - SKIPPED: " + testResultAction.skipCount);
        println("TESTS - FAILED: " + testResultAction.failCount);
                
        if (failed > 0){
            return true;
        }

    } else {
        println("TESTS - REPORT NOT FOUND!!!");
        return true;
    }

    return false;
}
