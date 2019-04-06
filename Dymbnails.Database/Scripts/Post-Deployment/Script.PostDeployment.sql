:r .\InitData\Dymbnails\Preview.sql

INSERT INTO [dbo].[Dymbnails] (Title, Description, Content, RatingUp, RatingDown)
VALUES (
    'Test dumb 1', 'Testing dumb preview',
    N'<?xml version="1.0" encoding="utf-8"?>
      <xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
        <xsl:output method="xml" encoding="utf-8" />
        <xsl:template match="/">
          <basic:Canvas xmlns:basic="basic" xmlns:web="web"
                        Size="400 500" Background="Black">
            <basic:Rectangle Location="40" Size="320 400" Border="1 White" Padding="4">
              <web:Image Size="312 392" Sizing="Stretch" Source="http://i149.photobucket.com/albums/s51/miss-snezhok/sept_2008/a1db9f88.jpg" />
            </basic:Rectangle>

            <basic:Label Location="450 40" Text="Погладь кота, сцука!!11" Color="White" Font="Times New Roman, 22, style=Bold" />
          </basic:Canvas>
        </xsl:template>
      </xsl:stylesheet>',
     5, 1
)