<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST["cuid"];
$credit = $_POST["credit"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "UPDATE	character_list
        SET 	credit = $credit
		WHERE	character_uid = $cuid";
$result = mysqli_query($conn, $sql);


$sql = "SELECT	credit
		FROM 	character_list
		WHERE 	character_uid = $cuid";
$row = mysqli_fetch_assoc($result);

echo $row['credit'];

return;
?>