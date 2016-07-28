$ScriptPath = split-path -parent $MyInvocation.MyCommand.Definition
$DevToolsBinaries = "$ScriptPath\DevToolsBinaries"
$DBCTsPath = "$ScriptPath\DatabaseCompatibilityTests"

$repo = "http://kl-git-001/DevToolsBinaries.git"
if (Test-Path $DevToolsBinaries) {	
	pushd $DevToolsBinaries	
	$local=git rev-parse "@"
	$remote=git ls-remote $repo master
	$remote=$remote.substring(0,40)
	if ($local -eq $remote) {
		echo "Already up to date"
	} else {
		echo "DevTools are out of date - updating..."
		git pull
	}
	popd
} else {
	git clone $repo DevToolsBinaries
}

$repo = "http://kl-git-001/DBCTs.git"
if (Test-Path $DBCTsPath) {	
	pushd $DBCTsPath	
	$local=git rev-parse "@"
	$remote=git ls-remote $repo master
	$remote=$remote.substring(0,40)
	if ($local -eq $remote) {
		echo "Already up to date"
	} else {
		echo "DBCTs are out of date - updating..."
		git pull
	}
	popd
} else {
	git clone $repo DatabaseCompatibilityTests
}