GIT_USERNAME=gitName$Random
GIT_PASSWORD=Pa55w.rd

dotnet build
 
az webapp deployment user set --user-name $GIT_USERNAME --password $GIT_PASSWORD

az appservice plan create --name educationAppServicePlan --resource-group sqlrg --sku B1 --is-linux

az webapp create --resource-group sqlrg --plan educationAppServicePlan --name educationapp --runtime "DOTNETCORE|2.1" --deployment-local-git

git remote add azure https://$GIT_USERNAME@educationapp.scm.azurewebsites.net/educationapp.git

git push azure master

echo '**DONE**'