<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST["cuid"];
$questid = $_POST["questid"];
$current = $_POST["current"];
$isdone = $_POST["isdone"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "INSERT INTO player_questdata (character_uid, quest_id, CURRENT, isdone)
		VALUES ($cuid, $questid, $current, $isdone)";

$result = mysqli_query($conn, $sql);

if ($result) {
	echo 1;
} else {
	echo 0;
}

mysqli_close($conn);
?>