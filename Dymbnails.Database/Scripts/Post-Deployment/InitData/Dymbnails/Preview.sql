-- Preview dymbnail
SET IDENTITY_INSERT [dbo].[Dymbnails] ON

INSERT INTO [dbo].[Dymbnails] (ID, Title, Description, Content)
VALUES (
    1, 'Preview', 'Dymbnail preview, 150x150 pixels.',
    N'<?xml version="1.0" encoding="utf-8"?>
      <xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
        <xsl:output method="xml" encoding="utf-8" />
        <xsl:param name="image-id" />
        
        <xsl:template match="/">
          <web:Image xmlns:web="web" Size="150 150" Sizing="Scale" Source="http://localhost:2288/{$image-id}" />
        </xsl:template>
      </xsl:stylesheet>'
)

SET IDENTITY_INSERT [dbo].[Dymbnails] OFF
SET IDENTITY_INSERT [dbo].[Variables] ON

INSERT INTO [dbo].[Variables] (ID, DymbnailID, [Name], Title, Description)
    VALUES (1, 1, 'image-id', 'Image ID', 'Dymbnail image Id to generate preview from.')

SET IDENTITY_INSERT [dbo].[Variables] OFF
