<?php
$servername = "localhost";
$username = "root";
$password = "1234";
$dbname = "fps_game";

$id = $_POST["id"];

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    echo "Failed to connect to MySQL : " + $mysqli->connect_error;
    exit();
}

$sql = "select currentTime
from user
where username = 'user1'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) > 0) {
    if ($row = mysqli_fetch_assoc($result)) {
        echo $row['currentTime'];
    }
}

$sql = "update user
        set currentTime = now()
        where username = '$id';";

mysqli_query($conn, $sql);

return;
?>