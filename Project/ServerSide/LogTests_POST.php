<?php
	$file_name = $_POST["_fileName"];
	$content = $_POST["_content"];

	file_put_contents("Data/" . $file_name, $content, FILE_APPEND);
?>