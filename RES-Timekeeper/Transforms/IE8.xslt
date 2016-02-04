<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    <Projects>
    <xsl:variable name="dataTableRows" select="//table[@id='b_s10_g10s108']//tr[starts-with(@id,'b_s10_g10s108__row')]" />
    <xsl:for-each select="$dataTableRows">
      <Project>
        <xsl:variable name="divsInRow" select=".//div[starts-with(@id, 'b_s10_g10s108__row')]" />
          <xsl:for-each select="$divsInRow">
            <xsl:if test="substring(@id, string-length(@id) - 7) = '_ctl00_c'">
              <ProjectRowID><xsl:value-of select="./@id"/></ProjectRowID>
              <ProjectCode><xsl:value-of select="./text()"/></ProjectCode>
            </xsl:if>
            <xsl:if test="substring(@id, string-length(@id) - 7) = '_ctl01_c'">
              <ProjectDescription><xsl:value-of select="./text()"/></ProjectDescription>
            </xsl:if>
          </xsl:for-each>
      </Project>
    </xsl:for-each>
    </Projects>
  </xsl:template>
</xsl:stylesheet>
