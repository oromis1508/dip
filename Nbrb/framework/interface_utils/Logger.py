import logging


class Logger:

    @staticmethod
    def add_log(class_name='', log_type='info', message=''):
        log_message = '%{class_name} -- %{message}'.format(class_name=class_name, message=message)

        if log_type == 'info':
            logging.info(msg=log_message)
        elif log_type == 'warn':
            logging.warning(msg=log_message)
        elif log_type == 'error':
            logging.error(msg=log_message)
