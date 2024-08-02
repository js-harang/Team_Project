<?php
$servername = "192.168.52.187";
$username = "root";
$password = "1234";
$dbname = "team_project";

$unit_uid = $_POST["unit_uid"];
$name = $_POST["name"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    echo "Failed to connect to MySQL : " + $mysqli->connect_error;
}

$sql = "DELETE from character_list
        where character_uid = $unit_uid and name = '$name'";
$result = mysqli_query($conn, $sql);

if ($result) {
    echo "y";
} else {
    echo "n";
}

mysqli_close($conn);
?>