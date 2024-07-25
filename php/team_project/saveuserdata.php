<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$cuid = $_POST["cuid"];
$lv = $_POST["lv"];
$exp = $_POST["exp"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
	echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "UPDATE 	character_list
		SET		lv = $lv, exp = $exp
		WHERE 	character_uid = $cuid";

$result = mysqli_query($conn, $sql);

mysqli_close($conn);
?>