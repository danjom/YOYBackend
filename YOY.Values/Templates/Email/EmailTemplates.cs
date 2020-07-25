namespace YOY.Values.Templates.Email
{
    public static class EmailTemplates
    {

        #region WELCOME_EMAIL

        public const string WelcomePlain = "Hola {*USER_NAME*},\n\nMuchísimas gracias por unirte.\n\nNecesitamos que valides tu cuenta, accede este link {*VALIDATION_LINK*}";

        public const string WelcomeHTLM =
        "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n" +
        "<html xmlns=\"http://www.w3.org/1999/xhtml\">\n" +
            "<head>\n" +
                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\n" +
                "<!--[if !mso]><!-->\n" +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n" +
                "<!--<![endif]-->" +
                "<meta name = \"viewport\" content=\"width=device-width, initial-scale=1.0\">\n" +
                "<title></title>\n" +
                "<style type = \"text/css\" ></style>\n" +
                "<!--[if (gte mso 9)| (IE)]>\n" +
                "<style type=\"text/css\" >\n" +
                    "table{border-collapse: collapse !important;}\n" +
                "</style>\n" +
                "<![endif]-->\n" +
            "</head>\n" +
            "<style>\n" +
                "body{\n" +
                    "margin: 0 !important;\n" +
                    "padding: 0;\n" +
                    "background-color: #e1e1e1;\n" +
                "}\n" +
                "table{\n" +
                    "border-spacing: 0;\n" +
                    "font-family: sans-serif;\n" +
                    "color: #333333;/*background: #fff;*/\n" +
                "}\n" +
                "td{\n" +
                    "padding: 0;\n" +
                "}\n" +
                "img{\n" +
                    "border: 0;\n" +
                "}\n" +
                "div[style *= \"margin: 16px 0\"] {\n" +
                    "margin: 0 !important;\n" +
                "}\n" +
                ".wrapper{\n" +
                    "width: 100%;\n" +
                    "table-layout: fixed;\n" +
                    "-webkit-text-size-adjust: 100%;\n" +
                    "-ms-text-size-adjust: 100%;\n" +
                "}\n" +
                ".webkit{\n" +
                    "max-width: 600px;\n" +
                    "margin: 0 auto;\n" +
                "}\n" +
                ".outer{\n" +
                    "width: 100%;\n" +
                    "max-width: 600px;\n" +
                    "margin: 0 auto;\n" +
                "}\n" +
                ".full-width-image img{\n" +
                    "width: 100%;\n" +
                    "max-width: 600px;\n" +
                    "height: auto;\n" +
                "}\n" +
                ".inner{\n" +
                    "/*	padding: 10px;*/\n" +
                "}\n" +
                "p{\n" +
                    "margin: 0;\n" +
                "}\n" +
                "a{\n" +
                    "color: #ee6a56;\n" +
                    "text-decoration: underline;\n" +
                "}\n" +
                ".h1{\n" +
                    "font-size: 21px;\n" +
                    "font-weight: bold;\n" +
                    "margin-bottom: 18px;\n" +
                "}\n" +
                ".h2{\n" +
                    "font-size: 18px;\n" +
                    "font-weight: bold;\n" +
                    "margin-bottom: 12px;\n" +
                "}\n" +
                ".one-column.contents{\n" +
                    "text-align: left;\n" +
                "}\n" +
                ".one-column p{\n" +
                    "font-size: 14px;\n" +
                    "margin-bottom: 10px;\n" +
                "}\n" +
                ".two-column{\n" +
                    "text-align: center;\n" +
                    "font-size: 0;\n" +
                "}" +
                ".two-column.column {\n" +
                    "width: 100%;\n" +
                    "max-width: 300px;\n" +
                    "display: inline-block;\n" +
                    "vertical-align: top;\n" +
                "}" +
                ".contents {\n" +
                    "width: 100%;\n" +
                "}\n" +
                ".two-column.contents {\n" +
                    "font-size: 14px;\n" +
                    "text-align: left;\n" +
                "}\n" +
                ".two-column img{\n" +
                    "width: 100%;\n" +
                    "max-width: 280px;\n" +
                    "height: auto;\n" +
                "}\n" +
                ".two-column.text {\n" +
                    "padding-top: 10px;\n" +
                "}\n" +
                "@media screen and(max-width: 400px){\n" +
                    ".two-column.column {\n" +
                        "max-width: 100 % !important;\n" +
                    "}\n" +
                    ".two-column img {\n" +
                        "max - width: 100 % !important;\n" +
                    "}\n" +
                    ".subscribe {\n" +
                        "padding: 15px !important;\n" +
                        "text - align: center !important;\n" +
                    "}\n" +
                "}\n" +
                "@media screen and(min-width: 401px) and(max-width: 620px){\n" +
                    ".two-column.column {\n" +
                        "max-width: 50 % !important;\n" +
                    "}\n" +
                "}\n" +
        "</style>\n" +
        "<body style=\"margin-top: 0 !important;margin-bottom: 0 !important;margin-right: 0 !important;margin-left: 0 !important;padding-top: 0;padding-bottom: 0;padding-right: 0;padding-left: 0;background-color: #e1e1e1;padding: 0;margin: 0 !important;\">\n" +
            "<center class=\"wrapper\" style=\"width: 100%; table-layout: fixed; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;\">\n" +
            "<div class=\"webkit\" style=\"max-width: 600px;margin-top: 0;margin-bottom: 0;margin-right: auto;margin-left: auto;margin: 0 auto;\">\n" +
            "<!--[if (gte mso 9)|(IE)]>\n" +
                "<table width=\"600\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;background-color:#fff;background-image:none;background-repeat:repeat;background-position:top left;background-attachment:scroll;\">\n" +
                    "<tr>\n" +
                        "<td style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
            "<![endif]-->\n" +
            "<table class=\"outer\" align=\"center\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-color: #fff;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;width: 100%;max-width: 600px;margin: 0 auto;\">\n" +
                "<tbody>\n" +
                    "<tr>\n" +
                        "<td class=\"full-width-image\" style=\"padding: 0;\">\n" +
                            "<a href=\"#\" target=\"_blank\" style=\"color: #ee6a56;text-decoration: underline;\">\n" +
                                "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923338/email_assets/img-header-html-yoy.png\" width=\"600\" height=\"103\" alt=\"Club YOY\" style=\"border-width: 0;width: 100%;max-width: 600px;height: auto;border: 0;\">\n" +
                            "</a>\n" +
                        "</td>\n" +
                    "</tr>\n" +
                    "<tr>\n" +
                        "<td class=\"one-column\" style=\"padding: 0;\">\n" +
                            "<table width=\"100%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-color: #fff;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;\">\n" +
                                "<tbody>\n" +
                                    "<tr>\n" +
                                        "<td class=\"inner contents\" style=\"width: 100%; text-align: left; padding: 40px;\">\n" +
                                            "<p class=\"h1\" style=\"font-family: 'Montserrat', sans-serif;font-size: 35px;font-weight: bold;color: #262323;text-decoration: none;letter-spacing: -2px;margin: 0 0 15px 0;padding: 0;border-bottom: 2px solid #c41013;margin-bottom: 10px;\">\n" +
                                                "¡Gracias, {*USER_NAME*}!\n" +
                                            "</p>\n" +
                                            "<ul style =\"font-family: 'Montserrat', sans-serif; font-size: 15px; letter-spacing:-0.5px; color: #1e1a1a; line-height: 28px; margin: 0; margin:0; padding:0; text-align:justify; list-style:none;\">\n" +
                                                "<li><span style=\"font-size: 20px; color: #c7262a; margin: 0; padding: 0; vertical-align: middle;\">•</span> YOY es tu <strong> mejor compañero de compras</strong>, innovamos la forma de comprar.</li>\n" +
                                                "<li><span style=\"font-size: 20px; color: #c7262a; margin: 0; padding: 0; vertical-align: middle;\">•</span> Te regresamos<strong> DINERO REAL</strong> por tus compras.</li>\n" +
                                                "<li><span style=\"font-size: 20px; color: #c7262a; margin: 0; padding: 0; vertical-align: middle;\">•</span> Te ayudamos a descubrir<strong> las mejores sorpesas y promos </strong>.</li>\n" +
                                                "<li><span style=\"font-size: 20px; color: #c7262a; margin: 0; padding: 0; vertical-align: middle;\">•</span> <strong>Descubre lo mejor</strong>, cuando realmente lo necesitas.</li>\n" +
                                                "<li><span style=\"font-size: 20px; color: #c7262a; margin: 0; padding: 0; vertical-align: middle;\">•</span>Existimos porque <strong>nos importas</strong>, nuestra misión es siempre <strong>darte más por tu dinero</strong>.</li>\n" +
                                            "</ul>\n" +
                                        "</td>\n" +
                                    "</tr>\n" +
                                "</tbody>\n" +
                            "</table>\n" +
                        "</td>\n" +
                    "</tr>\n" +
                    "<tr>\n" +
                        "<td class=\"one-column\" style=\"padding: 0;\">\n" +
                            "<table width=\"100%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-color: #f6f6f6;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;\">\n" +
                                "<tbody>\n" +
                                    "<tr>\n" +
                                        "<td class=\"inner contents\" style=\"width: 100%; text-align: center; padding: 40px 30px;\">\n" +
                                            "<a href = \"{*VALIDATION_LINK*}\" target=\"_blank\" style=\"background: #c41013;font-family: 'Montserrat', sans-serif; font-size: 16px; font-weight: 700; color: #ffff; text-align: center; text-transform: uppercase; margin: 0 auto; padding: 15px 40px; text-decoration: none;\">\n" +
                                                "Validar Mi Cuenta\n" +
                                            "</a>\n" +
                                        "</td>\n" +
                                    "</tr>\n" +
                                "</tbody>\n" +
                            "</table>\n" +
                         "</td>\n" +
                    "</tr>\n" +
                    "<tr>\n" +
                        "<td class=\"two-column\" style=\"text-align: center;font-size: 0;padding: 0;background-color: #c92323;\">\n" +
                            "<!--[if (gte mso 9)|(IE)]>\n" +
                            "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;background-color:#fff;background-image:none;background-repeat:repeat;background-position:top left;background-attachment:scroll;\">\n" +
                                "<tr>\n" +
                                    "<td width=\"50%\" valign=\"top\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
                            "<![endif]-->\n" +
                            "<div class=\"column\" style=\"width: 100%; max-width: 300px; display: inline-block; vertical-align: top;\">\n" +
                                "<table width=\"100%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;\">\n" +
                                    "<tbody>\n" +
                                        "<tr>\n" +
                                            "<td class=\"inner\" style=\"padding: 10px;\">\n" +
                                                "<table class=\"contents\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;width: 100%;font-size: 14px;text-align: left;\">\n" +
                                                    "<tbody>\n" +
                                                        "<tr>\n" +
                                                            "<td class=\"text subscribe\" style=\"padding: 15px 0 0 30px;padding-top: 10px;\">\n" +
                                                                "<p style=\"font-family: 'Montserrat', sans-serif;font-size: 30px;font-weight: 700;line-height: 33px;letter-spacing: -1px;color: #fff;margin: 0;\">\n" +
                                                                    "<span style=\"color:#e8c01c;\">Vive la experiencia</span>\n" +
                                                                    "<br>\n" +
                                                                    "con tu celular\n" +
                                                                "</p>\n" +
                                                                "<p style=\"font-family: 'Montserrat', sans-serif; font-size: 14px; color: #fff; line-height: 20px; margin: 0 0 30px 0;\">\n" +
                                                                    "Las mejores promos y sorpresas de tus comercios favoritos.\n" +
                                                                "</p>\n" +
                                                                "<a href=\"{*WEBSITE*}\" target=\"_blank\" style=\"background: #e8c01c; font-family: 'Montserrat', sans-serif; font-size: 16px; font-weight: 700; color: #c92323; text-align: center; text-transform: uppercase; margin: 0 auto; padding: 10px 30px; text-decoration: none;\">\n" +
                                                                    "Visitar Sitio\n" +
                                                                "</a>\n" +
                                                            "</td>\n" +
                                                        "</tr>\n" +
                                                    "</tbody>\n" +
                                                "</table>\n" +
                                            "</td>\n" +
                                        "</tr>\n" +
                                    "</tbody>\n" +
                                "</table>\n" +
                            "</div>\n" +
                            "<!--[if (gte mso 9)| (IE)]>\n" +
                            "</td>\n" +
                            "<td width = \"50%\" valign=\"top\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
                            "<![endif]-->\n" +
                            "<div class=\"column\" style=\"width: 100%; max-width: 300px; display: inline-block; vertical-align: top;\">\n" +
                                "<table width=\"100%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;\">\n" +
                                    "<tbody>\n" +
                                        "<tr>\n" +
                                            "<td class=\"inner\" style=\"padding: 0;\">\n" +
                                                "<table class=\"contents\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;width: 100%;font-size: 14px;text-align: left;\">\n" +
                                                    "<tbody>\n" +
                                                        "<tr>\n" +
                                                            "<td style=\"padding: 0;\">\n" +
                                                                "<a href=\"#\" target=\"_blank\" style=\"color: #ee6a56; text-decoration: underline;\">\n" +
                                                                    "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923338/email_assets/yoy-logo-mascota-2019.png\" width=\"397\" alt=\"\" style=\"border-width: 0;width: 100%;max-width: 280px;height: auto;border: 0;\">\n" +
                                                                "</a>\n" +
                                                            "</td>\n" +
                                                        "</tr>\n" +
                                                    "</tbody>\n" +
                                                "</table>\n" +
                                            "</td>\n" +
                                        "</tr>\n" +
                                    "</tbody>\n" +
                                "</table>\n" +
                            "</div>\n" +
                            "<!--[if (gte mso 9)|(IE)]>\n" +
                                    "</td>\n" +
                                "</tr>\n" +
                            "</table>\n" +
                            "<![endif]-->" +
                            "</td>\n" +
                        "</tr>\n" +
                        "<tr>\n" +
                            "<td class=\"one-column\" style=\"padding-top: 0;padding-bottom: 0px;padding-right: 0;padding-left: 0;padding: 0;\">\n" +
                                "<table width=\"100%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-color: #fff;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;\">\n" +
                                    "<tbody>\n" +
                                        "<tr>\n" +
                                            "<td class=\"inner contents\" style=\"padding: 40px 0 20px 0;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;width: 100%;text-align: left;\">\n" +
                                                "<p style=\"font-family: 'Montserrat', sans-serif;font-size: 17px;font-weight: 700;text-decoration: none;margin: 0;text-align: center;text-transform: uppercase;color: #262323;letter-spacing: -0.5px;margin-bottom: 10px;\">\n" +
                                                    "Encuéntranos en:\n" +
                                                "</p>\n" +
                                            "</td>\n" +
                                        "</tr>\n" +
                                    "</tbody>\n" +
                                "</table>\n" +
                            "</td>\n" +
                        "</tr>\n" +
                        "<tr>\n" +
                            "<td class=\"three-column\" style=\"padding-top: 0;padding-bottom: 30px;padding-right: 0;padding-left: 0;background-color: #ffffff;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;text-align: center;font-size: 0;padding: 0 0 30px 0;\">\n" +
                                "<!--[if (gte mso 9)|(IE)]>\n" +
                                   "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;\">\n" +
                                        "<tr>\n" +
                                            "<td width=\"200\" valign=\"top\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
                                "<![endif]-->\n" +
                                "<div class=\"column\" style=\"width: 100%; max-width: 200px; display: inline-block; vertical-align: top;\">\n" +
                                    "<table width=\"75%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;margin: 0 auto;\">\n" +
                                        "<tbody>\n" +
                                            "<tr>\n" +
                                                "<td class=\"inner\" style=\"padding: 10px;\">\n" +
                                                    "<table class=\"contents\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;width: 100%;font-size: 14px;text-align: center;\">\n" +
                                                        "<tbody>\n" +
                                                            "<tr>\n" +
                                                                "<td style=\"padding: 0;\">\n" +
                                                                    "<a href=\"#\" style=\"color: #ee6a56;text-decoration: underline;\">\n" +
                                                                        "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923338/email_assets/img-google-play-yoy-html.jpg\" width=\"135\" height=\"41\" alt=\"\" style=\"border-width: 0;max-width: 135px;height: auto;border: 0;\">\n" +
                                                                    "</a>\n" +
                                                                "</td>\n" +
                                                            "</tr>\n" +
                                                        "</tbody>\n" +
                                                    "</table>\n" +
                                                "</td>\n" +
                                            "</tr>\n" +
                                        "</tbody>\n" +
                                    "</table>\n" +
                                "</div>\n" +
                                "<!--[if (gte mso 9)|(IE)]>\n" +
                                    "</td>\n" +
                                    "<td width=\"200\" valign=\"top\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
                                "<![endif]-->\n" +
                                "<div class=\"column\" style=\"width: 100%; max-width: 200px; display: inline-block; vertical-align: top;\">\n" +
                                    "<table width=\"75%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;margin: 0 auto;\">\n" +
                                        "<tbody>\n" +
                                            "<tr>\n" +
                                                "<td class=\"inner\" style=\"padding: 10px;\">\n" +
                                                    "<table class=\"contents\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;font-size: 14px;text-align: center;width: 100%;\">\n" +
                                                        "<tbody>\n" +
                                                            "<tr>\n" +
                                                                "<td style = \"padding: 0;\">\n" +
                                                                    "<a href=\"#\" target=\"_blank\" style=\"color: #ee6a56; text-decoration: underline;\">\n" +
                                                                        "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923430/email_assets/img-apple-store-yoy-html.jpg\" width=\"135\" height=\"41\" alt=\"\" style=\"border-width: 0;width: 100%;max-width: 135px;height: auto;border: 0;\">\n" +
                                                                    "</a>\n" +
                                                                "</td>\n" +
                                                            "</tr>\n" +
                                                        "</tbody>\n" +
                                                    "</table>\n" +
                                                "</td>\n" +
                                            "</tr>\n" +
                                        "</tbody>\n" +
                                    "</table>\n" +
                                "</div>\n" +
                                "<!--[if (gte mso 9)|(IE)]>\n" +
                                "</td>\n" +
                                "<td width=\"200\" valign=\"top\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
                                "<![endif]-->\n" +
                                    "<div class=\"column\" style=\"width: 100%; max-width: 200px; display: inline-block; vertical-align: top;\">\n" +
                                        "<table width=\"75%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;margin: 0 auto;\">\n" +
                                            "<tbody>\n" +
                                                "<tr>\n" +
                                                    "<td class=\"inner\" style=\"padding: 10px;\">\n" +
                                                        "<table class=\"contents\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;font-size: 14px;text-align: center;width: 100%;\">\n" +
                                                            "<tbody>\n" +
                                                                "<tr>\n" +
                                                                    "<td style =\"padding: 0;\">\n" +
                                                                        "<a href=\"#\" style=\"color: #ee6a56;text-decoration: underline;\">\n" +
                                                                            "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923477/email_assets/img-facebook-yoy-html.jpg\" width=\"36\" height=\"41\" alt=\"\" style=\"border-width: 0;max-width: 36px;height: auto;border: 0;\">\n" +
                                                                        "</a>\n" +
                                                                    "</td>\n" +
                                                                    "<td style = \"padding: 0;\">\n" +
                                                                        "<a href=\"#\" style=\"color: #ee6a56;text-decoration: underline;\">\n" +
                                                                            "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923528/email_assets/img-google-plus-yoy-html.jpg\" width=\"36\" height=\"41\" alt=\"\" style=\"border-width: 0;max-width: 36px;height: auto;border: 0;\">\n" +
                                                                        "</a>\n" +
                                                                    "</td>\n" +
                                                                "</tr>\n" +
                                                            "</tbody>\n" +
                                                        "</table>\n" +
                                                    "</td>\n" +
                                                "</tr>\n" +
                                            "</tbody>\n" +
                                        "</table>\n" +
                                    "</div>\n" +
                                "<!--[if (gte mso 9)|(IE)]>\n" +
                                "</td>\n" +
                            "</tr>\n" +
                        "</table>\n" +
                    "<![endif]-->\n" +
                    "</td>\n" +
                "</tr>\n" +
                "<tr>\n" +
                    "<td class=\"full-width-image\" style=\"padding: 0;\">\n" +
                        "<a href=\"#\" target=\"_blank\" style=\"color: #ee6a56;text-decoration: underline;\">\n" +
                            "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923575/email_assets/img-footer-html-yoy.png\" width=\"600\" height=\"70\" alt=\"YOY Club\" style=\"border-width: 0;width: 100%;max-width: 600px;height: auto;border: 0;\">\n" +
                        "</a>\n" +
                    "</td>\n" +
                "</tr>\n" +
            "</tbody>\n" +
        "</table>\n" +
    "</div>\n" +
    "</center>\n" +
"</body>\n" +
"</html>\n";

        #endregion

        #region PASSWORD_RECOVERY

        public const string PasswordRecoveryPlain = "Hola,\n\nHemos recibido tu solicitud para recuperar tu contraseña.\n\nNecesitamos que accedas a este link {*VALIDATION_LINK*}";

        public const string PasswordRecoveryHTLM =
        "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n" +
        "<html xmlns=\"http://www.w3.org/1999/xhtml\">\n" +
            "<head>\n" +
                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\n" +
                "<!--[if !mso]><!-->\n" +
                "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n" +
                "<!--<![endif]-->" +
                "<meta name = \"viewport\" content=\"width=device-width, initial-scale=1.0\">\n" +
                "<title></title>\n" +
                "<style type = \"text/css\" ></style>\n" +
                "<!--[if (gte mso 9)| (IE)]>\n" +
                "<style type=\"text/css\" >\n" +
                    "table{border-collapse: collapse !important;}\n" +
                "</style>\n" +
                "<![endif]-->\n" +
            "</head>\n" +
            "<style>\n" +
                "body{\n" +
                    "margin: 0 !important;\n" +
                    "padding: 0;\n" +
                    "background-color: #e1e1e1;\n" +
                "}\n" +
                "table{\n" +
                    "border-spacing: 0;\n" +
                    "font-family: sans-serif;\n" +
                    "color: #333333;/*background: #fff;*/\n" +
                "}\n" +
                "td{\n" +
                    "padding: 0;\n" +
                "}\n" +
                "img{\n" +
                    "border: 0;\n" +
                "}\n" +
                "div[style *= \"margin: 16px 0\"] {\n" +
                    "margin: 0 !important;\n" +
                "}\n" +
                ".wrapper{\n" +
                    "width: 100%;\n" +
                    "table-layout: fixed;\n" +
                    "-webkit-text-size-adjust: 100%;\n" +
                    "-ms-text-size-adjust: 100%;\n" +
                "}\n" +
                ".webkit{\n" +
                    "max-width: 600px;\n" +
                    "margin: 0 auto;\n" +
                "}\n" +
                ".outer{\n" +
                    "width: 100%;\n" +
                    "max-width: 600px;\n" +
                    "margin: 0 auto;\n" +
                "}\n" +
                ".full-width-image img{\n" +
                    "width: 100%;\n" +
                    "max-width: 600px;\n" +
                    "height: auto;\n" +
                "}\n" +
                ".inner{\n" +
                    "/*	padding: 10px;*/\n" +
                "}\n" +
                "p{\n" +
                    "margin: 0;\n" +
                "}\n" +
                "a{\n" +
                    "color: #ee6a56;\n" +
                    "text-decoration: underline;\n" +
                "}\n" +
                ".h1{\n" +
                    "font-size: 21px;\n" +
                    "font-weight: bold;\n" +
                    "margin-bottom: 18px;\n" +
                "}\n" +
                ".h2{\n" +
                    "font-size: 18px;\n" +
                    "font-weight: bold;\n" +
                    "margin-bottom: 12px;\n" +
                "}\n" +
                ".one-column.contents{\n" +
                    "text-align: left;\n" +
                "}\n" +
                ".one-column p{\n" +
                    "font-size: 14px;\n" +
                    "margin-bottom: 10px;\n" +
                "}\n" +
                ".two-column{\n" +
                    "text-align: center;\n" +
                    "font-size: 0;\n" +
                "}" +
                ".two-column.column {\n" +
                    "width: 100%;\n" +
                    "max-width: 300px;\n" +
                    "display: inline-block;\n" +
                    "vertical-align: top;\n" +
                "}" +
                ".contents {\n" +
                    "width: 100%;\n" +
                "}\n" +
                ".two-column.contents {\n" +
                    "font-size: 14px;\n" +
                    "text-align: left;\n" +
                "}\n" +
                ".two-column img{\n" +
                    "width: 100%;\n" +
                    "max-width: 280px;\n" +
                    "height: auto;\n" +
                "}\n" +
                ".two-column.text {\n" +
                    "padding-top: 10px;\n" +
                "}\n" +
                "@media screen and(max-width: 400px){\n" +
                    ".two-column.column {\n" +
                        "max-width: 100 % !important;\n" +
                    "}\n" +
                    ".two-column img {\n" +
                        "max - width: 100 % !important;\n" +
                    "}\n" +
                    ".subscribe {\n" +
                        "padding: 15px !important;\n" +
                        "text - align: center !important;\n" +
                    "}\n" +
                "}\n" +
                "@media screen and(min-width: 401px) and(max-width: 620px){\n" +
                    ".two-column.column {\n" +
                        "max-width: 50 % !important;\n" +
                    "}\n" +
                "}\n" +
        "</style>\n" +
        "<body style=\"margin-top: 0 !important;margin-bottom: 0 !important;margin-right: 0 !important;margin-left: 0 !important;padding-top: 0;padding-bottom: 0;padding-right: 0;padding-left: 0;background-color: #e1e1e1;padding: 0;margin: 0 !important;\">\n" +
            "<center class=\"wrapper\" style=\"width: 100%; table-layout: fixed; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;\">\n" +
            "<div class=\"webkit\" style=\"max-width: 600px;margin-top: 0;margin-bottom: 0;margin-right: auto;margin-left: auto;margin: 0 auto;\">\n" +
            "<!--[if (gte mso 9)|(IE)]>\n" +
                "<table width=\"600\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;background-color:#fff;background-image:none;background-repeat:repeat;background-position:top left;background-attachment:scroll;\">\n" +
                    "<tr>\n" +
                        "<td style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
            "<![endif]-->\n" +
            "<table class=\"outer\" align=\"center\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-color: #fff;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;width: 100%;max-width: 600px;margin: 0 auto;\">\n" +
                "<tbody>\n" +
                    "<tr>\n" +
                        "<td class=\"full-width-image\" style=\"padding: 0;\">\n" +
                            "<a href=\"#\" target=\"_blank\" style=\"color: #ee6a56;text-decoration: underline;\">\n" +
                                "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923338/email_assets/img-header-html-yoy.png\" width=\"600\" height=\"103\" alt=\"Club YOY\" style=\"border-width: 0;width: 100%;max-width: 600px;height: auto;border: 0;\">\n" +
                            "</a>\n" +
                        "</td>\n" +
                    "</tr>\n" +
                    "<tr>\n" +
                        "<td class=\"one-column\" style=\"padding: 0;\">\n" +
                            "<table width=\"100%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-color: #fff;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;\">\n" +
                                "<tbody>\n" +
                                    "<tr>\n" +
                                        "<td class=\"inner contents\" style=\"width: 100%; text-align: left; padding: 40px;\">\n" +
                                            "<p class=\"h1\" style=\"font-family: 'Montserrat', sans-serif;font-size: 35px;font-weight: bold;color: #262323;text-decoration: none;letter-spacing: -2px;margin: 0 0 15px 0;padding: 0;border-bottom: 2px solid #c41013;margin-bottom: 10px;\">\n" +
                                                "¡Hola!\n" +
                                            "</p>\n" +
                                            "<ul style =\"font-family: 'Montserrat', sans-serif; font-size: 15px; letter-spacing:-0.5px; color: #1e1a1a; line-height: 28px; margin: 0; margin:0; padding:0; text-align:justify; list-style:none;\">\n" +
                                                "<li><span style=\"font-size: 20px; color: #c7262a; margin: 0; padding: 0; vertical-align: middle;\">•</span> Hemos recibido tu solicitud para reestablecer tu contraseña.</li>\n" +
                                                "<li><span style=\"font-size: 20px; color: #c7262a; margin: 0; padding: 0; vertical-align: middle;\">•</span> Por favor <strong>da click al botón de abajo</strong> para continuar con el proceso para reestablecerla.</li>\n" +
                                            "</ul>\n" +
                                        "</td>\n" +
                                    "</tr>\n" +
                                "</tbody>\n" +
                            "</table>\n" +
                        "</td>\n" +
                    "</tr>\n" +
                    "<tr>\n" +
                        "<td class=\"one-column\" style=\"padding: 0;\">\n" +
                            "<table width=\"100%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-color: #f6f6f6;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;\">\n" +
                                "<tbody>\n" +
                                    "<tr>\n" +
                                        "<td class=\"inner contents\" style=\"width: 100%; text-align: center; padding: 40px 30px;\">\n" +
                                            "<a href = \"{*VALIDATION_LINK*}\" target=\"_blank\" style=\"background: #c41013;font-family: 'Montserrat', sans-serif; font-size: 16px; font-weight: 700; color: #ffff; text-align: center; text-transform: uppercase; margin: 0 auto; padding: 15px 40px; text-decoration: none;\">\n" +
                                                "Reestablecer mi contraseña\n" +
                                            "</a>\n" +
                                        "</td>\n" +
                                    "</tr>\n" +
                                "</tbody>\n" +
                            "</table>\n" +
                         "</td>\n" +
                    "</tr>\n" +
                    "<tr>\n" +
                        "<td class=\"two-column\" style=\"text-align: center;font-size: 0;padding: 0;background-color: #c92323;\">\n" +
                            "<!--[if (gte mso 9)|(IE)]>\n" +
                            "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;background-color:#fff;background-image:none;background-repeat:repeat;background-position:top left;background-attachment:scroll;\">\n" +
                                "<tr>\n" +
                                    "<td width=\"50%\" valign=\"top\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
                            "<![endif]-->\n" +
                            "<div class=\"column\" style=\"width: 100%; max-width: 300px; display: inline-block; vertical-align: top;\">\n" +
                                "<table width=\"100%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;\">\n" +
                                    "<tbody>\n" +
                                        "<tr>\n" +
                                            "<td class=\"inner\" style=\"padding: 10px;\">\n" +
                                                "<table class=\"contents\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;width: 100%;font-size: 14px;text-align: left;\">\n" +
                                                    "<tbody>\n" +
                                                        "<tr>\n" +
                                                            "<td class=\"text subscribe\" style=\"padding: 15px 0 0 30px;padding-top: 10px;\">\n" +
                                                                "<p style=\"font-family: 'Montserrat', sans-serif;font-size: 30px;font-weight: 700;line-height: 33px;letter-spacing: -1px;color: #fff;margin: 0;\">\n" +
                                                                    "<span style=\"color:#e8c01c;\">Vive la experiencia</span>\n" +
                                                                    "<br>\n" +
                                                                    "con tu celular\n" +
                                                                "</p>\n" +
                                                                "<p style=\"font-family: 'Montserrat', sans-serif; font-size: 14px; color: #fff; line-height: 20px; margin: 0 0 30px 0;\">\n" +
                                                                    "Las mejores promos y sorpresas de tus comercios favoritos.\n" +
                                                                "</p>\n" +
                                                                "<a href=\"{*WEBSITE*}\" target=\"_blank\" style=\"background: #e8c01c; font-family: 'Montserrat', sans-serif; font-size: 16px; font-weight: 700; color: #c92323; text-align: center; text-transform: uppercase; margin: 0 auto; padding: 10px 30px; text-decoration: none;\">\n" +
                                                                    "Visitar Sitio\n" +
                                                                "</a>\n" +
                                                            "</td>\n" +
                                                        "</tr>\n" +
                                                    "</tbody>\n" +
                                                "</table>\n" +
                                            "</td>\n" +
                                        "</tr>\n" +
                                    "</tbody>\n" +
                                "</table>\n" +
                            "</div>\n" +
                            "<!--[if (gte mso 9)| (IE)]>\n" +
                            "</td>\n" +
                            "<td width = \"50%\" valign=\"top\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
                            "<![endif]-->\n" +
                            "<div class=\"column\" style=\"width: 100%; max-width: 300px; display: inline-block; vertical-align: top;\">\n" +
                                "<table width=\"100%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;\">\n" +
                                    "<tbody>\n" +
                                        "<tr>\n" +
                                            "<td class=\"inner\" style=\"padding: 0;\">\n" +
                                                "<table class=\"contents\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;width: 100%;font-size: 14px;text-align: left;\">\n" +
                                                    "<tbody>\n" +
                                                        "<tr>\n" +
                                                            "<td style=\"padding: 0;\">\n" +
                                                                "<a href=\"#\" target=\"_blank\" style=\"color: #ee6a56; text-decoration: underline;\">\n" +
                                                                    "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923338/email_assets/yoy-logo-mascota-2019.png\" width=\"397\" alt=\"\" style=\"border-width: 0;width: 100%;max-width: 280px;height: auto;border: 0;\">\n" +
                                                                "</a>\n" +
                                                            "</td>\n" +
                                                        "</tr>\n" +
                                                    "</tbody>\n" +
                                                "</table>\n" +
                                            "</td>\n" +
                                        "</tr>\n" +
                                    "</tbody>\n" +
                                "</table>\n" +
                            "</div>\n" +
                            "<!--[if (gte mso 9)|(IE)]>\n" +
                                    "</td>\n" +
                                "</tr>\n" +
                            "</table>\n" +
                            "<![endif]-->" +
                            "</td>\n" +
                        "</tr>\n" +
                        "<tr>\n" +
                            "<td class=\"one-column\" style=\"padding-top: 0;padding-bottom: 0px;padding-right: 0;padding-left: 0;padding: 0;\">\n" +
                                "<table width=\"100%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;background-color: #fff;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;\">\n" +
                                    "<tbody>\n" +
                                        "<tr>\n" +
                                            "<td class=\"inner contents\" style=\"padding: 40px 0 20px 0;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;width: 100%;text-align: left;\">\n" +
                                                "<p style=\"font-family: 'Montserrat', sans-serif;font-size: 17px;font-weight: 700;text-decoration: none;margin: 0;text-align: center;text-transform: uppercase;color: #262323;letter-spacing: -0.5px;margin-bottom: 10px;\">\n" +
                                                    "Encuéntranos en:\n" +
                                                "</p>\n" +
                                            "</td>\n" +
                                        "</tr>\n" +
                                    "</tbody>\n" +
                                "</table>\n" +
                            "</td>\n" +
                        "</tr>\n" +
                        "<tr>\n" +
                            "<td class=\"three-column\" style=\"padding-top: 0;padding-bottom: 30px;padding-right: 0;padding-left: 0;background-color: #ffffff;background-image: none;background-repeat: repeat;background-position: top left;background-attachment: scroll;text-align: center;font-size: 0;padding: 0 0 30px 0;\">\n" +
                                "<!--[if (gte mso 9)|(IE)]>\n" +
                                   "<table width=\"100%\" style=\"border-spacing:0;font-family:sans-serif;color:#333333;\">\n" +
                                        "<tr>\n" +
                                            "<td width=\"200\" valign=\"top\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
                                "<![endif]-->\n" +
                                "<div class=\"column\" style=\"width: 100%; max-width: 200px; display: inline-block; vertical-align: top;\">\n" +
                                    "<table width=\"75%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;margin: 0 auto;\">\n" +
                                        "<tbody>\n" +
                                            "<tr>\n" +
                                                "<td class=\"inner\" style=\"padding: 10px;\">\n" +
                                                    "<table class=\"contents\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;width: 100%;font-size: 14px;text-align: center;\">\n" +
                                                        "<tbody>\n" +
                                                            "<tr>\n" +
                                                                "<td style=\"padding: 0;\">\n" +
                                                                    "<a href=\"#\" style=\"color: #ee6a56;text-decoration: underline;\">\n" +
                                                                        "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923338/email_assets/img-google-play-yoy-html.jpg\" width=\"135\" height=\"41\" alt=\"\" style=\"border-width: 0;max-width: 135px;height: auto;border: 0;\">\n" +
                                                                    "</a>\n" +
                                                                "</td>\n" +
                                                            "</tr>\n" +
                                                        "</tbody>\n" +
                                                    "</table>\n" +
                                                "</td>\n" +
                                            "</tr>\n" +
                                        "</tbody>\n" +
                                    "</table>\n" +
                                "</div>\n" +
                                "<!--[if (gte mso 9)|(IE)]>\n" +
                                    "</td>\n" +
                                    "<td width=\"200\" valign=\"top\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
                                "<![endif]-->\n" +
                                "<div class=\"column\" style=\"width: 100%; max-width: 200px; display: inline-block; vertical-align: top;\">\n" +
                                    "<table width=\"75%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;margin: 0 auto;\">\n" +
                                        "<tbody>\n" +
                                            "<tr>\n" +
                                                "<td class=\"inner\" style=\"padding: 10px;\">\n" +
                                                    "<table class=\"contents\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;font-size: 14px;text-align: center;width: 100%;\">\n" +
                                                        "<tbody>\n" +
                                                            "<tr>\n" +
                                                                "<td style = \"padding: 0;\">\n" +
                                                                    "<a href=\"#\" target=\"_blank\" style=\"color: #ee6a56; text-decoration: underline;\">\n" +
                                                                        "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923430/email_assets/img-apple-store-yoy-html.jpg\" width=\"135\" height=\"41\" alt=\"\" style=\"border-width: 0;width: 100%;max-width: 135px;height: auto;border: 0;\">\n" +
                                                                    "</a>\n" +
                                                                "</td>\n" +
                                                            "</tr>\n" +
                                                        "</tbody>\n" +
                                                    "</table>\n" +
                                                "</td>\n" +
                                            "</tr>\n" +
                                        "</tbody>\n" +
                                    "</table>\n" +
                                "</div>\n" +
                                "<!--[if (gte mso 9)|(IE)]>\n" +
                                "</td>\n" +
                                "<td width=\"200\" valign=\"top\" style=\"padding-top:0;padding-bottom:0;padding-right:0;padding-left:0;\">\n" +
                                "<![endif]-->\n" +
                                    "<div class=\"column\" style=\"width: 100%; max-width: 200px; display: inline-block; vertical-align: top;\">\n" +
                                        "<table width=\"75%\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;margin: 0 auto;\">\n" +
                                            "<tbody>\n" +
                                                "<tr>\n" +
                                                    "<td class=\"inner\" style=\"padding: 10px;\">\n" +
                                                        "<table class=\"contents\" style=\"border-spacing: 0;font-family: sans-serif;color: #333333;font-size: 14px;text-align: center;width: 100%;\">\n" +
                                                            "<tbody>\n" +
                                                                "<tr>\n" +
                                                                    "<td style =\"padding: 0;\">\n" +
                                                                        "<a href=\"#\" style=\"color: #ee6a56;text-decoration: underline;\">\n" +
                                                                            "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923477/email_assets/img-facebook-yoy-html.jpg\" width=\"36\" height=\"41\" alt=\"\" style=\"border-width: 0;max-width: 36px;height: auto;border: 0;\">\n" +
                                                                        "</a>\n" +
                                                                    "</td>\n" +
                                                                    "<td style = \"padding: 0;\">\n" +
                                                                        "<a href=\"#\" style=\"color: #ee6a56;text-decoration: underline;\">\n" +
                                                                            "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923528/email_assets/img-google-plus-yoy-html.jpg\" width=\"36\" height=\"41\" alt=\"\" style=\"border-width: 0;max-width: 36px;height: auto;border: 0;\">\n" +
                                                                        "</a>\n" +
                                                                    "</td>\n" +
                                                                "</tr>\n" +
                                                            "</tbody>\n" +
                                                        "</table>\n" +
                                                    "</td>\n" +
                                                "</tr>\n" +
                                            "</tbody>\n" +
                                        "</table>\n" +
                                    "</div>\n" +
                                "<!--[if (gte mso 9)|(IE)]>\n" +
                                "</td>\n" +
                            "</tr>\n" +
                        "</table>\n" +
                    "<![endif]-->\n" +
                    "</td>\n" +
                "</tr>\n" +
                "<tr>\n" +
                    "<td class=\"full-width-image\" style=\"padding: 0;\">\n" +
                        "<a href=\"#\" target=\"_blank\" style=\"color: #ee6a56;text-decoration: underline;\">\n" +
                            "<img src=\"https://res.cloudinary.com/yoyimgs/image/upload/v1574923575/email_assets/img-footer-html-yoy.png\" width=\"600\" height=\"70\" alt=\"YOY Club\" style=\"border-width: 0;width: 100%;max-width: 600px;height: auto;border: 0;\">\n" +
                        "</a>\n" +
                    "</td>\n" +
                "</tr>\n" +
            "</tbody>\n" +
        "</table>\n" +
    "</div>\n" +
    "</center>\n" +
"</body>\n" +
"</html>\n";

        #endregion

    }
}
