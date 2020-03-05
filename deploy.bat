docker build -t sdg-hockey-sticks-image .

docker tag sdg-hockey-sticks-image registry.heroku.com/sdg-hockey-sticks/web

docker push registry.heroku.com/sdg-hockey-sticks/web

heroku container:release web -a sdg-hockey-sticks