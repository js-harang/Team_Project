<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST["cuid"];
$type = $_POST["type"];
$value = $_POST["value"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

if ($type == 0) {
	$type = "lv";
} else if ($type == 1) {
	$type = "exp";
} else if ($type == 2) {
	$type = "credit";
}

lv_or_exp($conn, $cuid, $type, $value);

mysqli_close($conn);

function lv_or_exp($conn, $cuid, $type, $value)
{
	$sql = "UPDATE 	character_list
			SET		$type = $value
			WHERE 	character_uid = $cuid";

	mysqli_query($conn, $sql);
}
?>