(docker ps -q | ForEach-Object { docker inspect $_ --format '{{ .NetworkSettings.Networks.nat.IPAddress }} ... {{ .Name }}' }).replace('/','')