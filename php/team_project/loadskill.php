<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_GET["cuid"];
$skill = $_GET["skill"];
$tbl = "character_info";

$conn = new mysqli($servername, $username, $password, $dbname);
if ($conn->connect_error) {
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "select	$skill
		from	$tbl
		where	character_uid = '0000000013'";
$result = mysqli_query($conn, $sql);
$row = mysqli_fetch_assoc($result);
echo $row['skill_0'];

return;
?>