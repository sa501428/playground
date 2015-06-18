SQL export 

SELECT * INTO OUTFILE '/tmp/table.csv'
    FIELDS TERMINATED BY ',' OPTIONALLY ENCLOSED BY '"'
    LINES TERMINATED BY '\n'
FROM Newsletter;

# For webserver
sudo chmod 755 /var/www/
sudo chmod 644 /var/www/*.*
vim /etc/apache2/httpd.conf
sudo /etc/init.d/apache2 reload

// in the logs/newSync folder 
aws s3 sync s3://hicfiles.logs localLog/


vim /Users/muhammadsaadshamim/.ssh/config

<script>
      window.fbAsyncInit = function() {
        FB.init({
          appId      : '',
          xfbml      : true,
          version    : 'v2.1'
        });
      };

      (function(d, s, id){
         var js, fjs = d.getElementsByTagName(s)[0];
         if (d.getElementById(id)) {return;}
         js = d.createElement(s); js.id = id;
         js.src = "//connect.facebook.net/en_US/sdk.js";
         fjs.parentNode.insertBefore(js, fjs);
       }(document, 'script', 'facebook-jssdk'));
</script>


<!-- Piwik -->
<script type="text/javascript">
  var _paq = _paq || [];
  _paq.push(['trackPageView']);
  _paq.push(['enableLinkTracking']);
  (function() {
    var u="//54.172.203.213/piwik/";
    _paq.push(['setTrackerUrl', u+'piwik.php']);
    _paq.push(['setSiteId', 1]);
    var d=document, g=d.createElement('script'), s=d.getElementsByTagName('script')[0];
    g.type='text/javascript'; g.async=true; g.defer=true; g.src=u+'piwik.js'; s.parentNode.insertBefore(g,s);
  })();
</script>
<noscript><p><img src="//54.172.203.213/piwik/piwik.php?idsite=1" style="border:0;" alt="" /></p></noscript>
<!-- End Piwik Code --> 


scp -i /Users/muhammadsaadshamim/Desktop/keys/bookworm-keypair.pem piwik.zip ubuntu@54.172.203.213:piwik.zip

-A -i Desktop/keys/bookworm-keypair.pem mshamim@sullivan.opensciencedatacloud.org


(22 Mbps)*(3600 s/hr)*(0.001 )($0.120 per GB)

ssh mshamim@54.160.220.95

scp -i Desktop/keys/osdc-keypair.pem Desktop/keys/osdc-keypair.pem mshamim@sullivan.opensciencedatacloud.org:osdc-keypair.pem

update bookworms set status = "Waiting" where bookwormname = "obo_little_test_3" where bookwormname = "obo little test 3";


java -Xms512m -Xmx2048m -jar out/Juicebox_clt_jar/Juicebox_CLT.jar pre testing/HIC156_smaller.txt.gz testing/HIC156_smaller_test.hic hg19


sudo launchctl unload -w /System/Library/LaunchDaemons/com.apple.metadata.mds.plist
sudo launchctl load -w /System/Library/LaunchDaemons/com.apple.metadata.mds.plist
