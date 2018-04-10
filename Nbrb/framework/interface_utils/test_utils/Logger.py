import logging


class Logger:

    @staticmethod
    def add_log(class_name='root', log_type='info', message=''):
        """ Add the message to the log

        :param class_name: name of the class or the file where log has been added
        :param log_type: level of the log message
        :param message: added message (in format class_name -- message)
        """
        log_message = '%{class_name} -- %{message}'.format(class_name=class_name, message=message)

        if log_type == 'info':
            logging.info(msg=log_message)
        elif log_type == 'warn':
            logging.warning(msg=log_message)
        elif log_type == 'error':
            logging.error(msg=log_message)
        else:
            logging.info('{type} log type is incorrect'.format(type=log_type))
            logging.info(msg=log_message)
