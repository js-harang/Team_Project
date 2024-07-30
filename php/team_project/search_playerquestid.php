<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST["cuid"];
$questID = $_POST["questID"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "SELECT 	quest_id
		FROM 	player_questdata
		WHERE 	character_uid = $cuid and quest_id = $questID";

$result = mysqli_query($conn, $sql);

mysqli_close($conn);
?>