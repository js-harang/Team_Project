<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST["cuid"];
$skill = $_POST["skill"];
$idx = $_POST["idx"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "UPDATE 	character_info
		SET		'".$skill."' = '".$idx."'
		WHERE 	character_uid = '".$cuid."'";
		
$result = mysqli_query($conn, $sql);

$sql = "select	$skill
		from	character_info
		where	character_uid = '0000000013'";
		
$result = mysqli_query($conn, $sql);
$row = mysqli_fetch_assoc($result);
echo $row[$skill];

return;
?>